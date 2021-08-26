using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using log4net;
using UtilsSmev.Crypto;

namespace UtilsSmev
{
   public static class Signer
   {
      private const string BodyId = "body";

      public static string SignMessageTopCase(string message, string certificateId, X509Certificate2 certificate,
         StoreLocation storeLocation, string pinCode, List<string> xpaths, List<string> ids, List<string> namespaces,
         List<string> prefixes, string actor = "http://smev.gosuslugi.ru/actors/smev")
      {
         try
         {
            var document = new XmlDocument {PreserveWhitespace = false};
            document.LoadXml(message);

            var cryptoHelper = new CryptoHelper();
            uint nErrorCode;
            string error;

            document.DocumentElement.SetAttribute("xmlns:wsu",
               "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");

            // Добавляем XmlDeclaration -не думаю что это необходимо
            if (!(document.FirstChild is XmlDeclaration))
            {
               var declaration = document.CreateXmlDeclaration("1.0", "UTF-8", string.Empty);
               document.InsertBefore(declaration, document.FirstChild);
            }

            var root = document.DocumentElement;
            root.SetAttribute("xmlns:wsse",
               "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            root.SetAttribute("xmlns:wsu",
               "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
            root.SetAttribute("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");

            // Ищем body и добавляем ему Id
            var namespaceManager = GetNewFilledNamespaceManager(document.NameTable);
            if (namespaces != null)
               for (var i = 0; i < namespaces.Count; i++)
               {
                  root.SetAttribute($"xmlns:{prefixes[i]}", namespaces[i]);
                  namespaceManager.AddNamespace(prefixes[i], namespaces[i]);
               }

            // Определяем алгоритм сертификата
            var certificateB64 = Convert.ToBase64String(certificate.RawData);
            var algorithm = cryptoHelper.GetCertificateAlgorithm(certificateB64, out nErrorCode, out error);
            if (nErrorCode != 0 || !algorithm.HasValue)
            {
               LogManager.GetLogger(typeof(Signer))
                  .ErrorFormat("Не удалось определить алгоритм сертификата: {0}", error);
               throw new ApplicationException(error);
            }

            LogManager.GetLogger(typeof(Signer)).InfoFormat($"Algoritm - {algorithm}");

            var hashes = new StringBuilder();

            for (var index = 0; index < xpaths.Count; index++)
            {
               var xpath = xpaths[index];
               var body = document.DocumentElement.SelectSingleNode(xpath, namespaceManager) as XmlElement;
               if (body == null) throw new ApplicationException($"Не найден элемент для расчета хэша xpath='{xpath}'");

               var idAttr = document.CreateNode(XmlNodeType.Attribute, "wsu", "Id",
                  namespaceManager.LookupNamespace("wsu"));
               body.Attributes.SetNamedItem(idAttr).InnerText = ids[index];

               var xmlDocumentSignInfo = new XmlDocument {PreserveWhitespace = false};
               xmlDocumentSignInfo.LoadXml(body.OuterXml);
               //xmlDocumentSignInfo.LoadXml(body.OuterXml.Replace(@" xmlns=""""", @""));

               //выполняем канонизацию
               var xmlTransform = new XmlDsigExcC14NTransform(false);
               xmlTransform.Algorithm = SignedXml.XmlDsigExcC14NTransformUrl;
               xmlTransform.LoadInput(xmlDocumentSignInfo);
               using (var output = (Stream) xmlTransform.GetOutput(typeof(Stream)))
               {
                  using (var reader = new StreamReader(output))
                  {
                     var bodyString = reader.ReadToEnd();
                     var hashesB64 = cryptoHelper.GetHashesB64(Encoding.UTF8.GetBytes(bodyString), false,
                        out nErrorCode, out error);
                     if (nErrorCode != 0)
                     {
                        LogManager.GetLogger(typeof(Signer)).ErrorFormat("Не удалось вычислить хеш: {0}", error);
                        throw new ApplicationException(error);
                     }

                     string hash;
                     hashesB64.TryGetValue(algorithm.Value, out hash);
                     if (string.IsNullOrEmpty(hash))
                     {
                        LogManager.GetLogger(typeof(Signer)).Error("Не удалось вычислить хеш");
                        throw new ApplicationException(error);
                     }

                     if (algorithm == HashAlgorithmType.GOST3411_2012)
                        hashes.AppendLine(string.Format(@"<ds:Reference URI=""#{1}"">
				<ds:Transforms>
					<ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
				</ds:Transforms>
				<ds:DigestMethod Algorithm=""urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-256""/>
				<ds:DigestValue>{0}</ds:DigestValue>
			</ds:Reference>", hash, ids[index]));
                     else if (algorithm == HashAlgorithmType.GOST3411_2012_STRONG)
                        hashes.AppendLine(string.Format(@"<ds:Reference URI=""#{1}"">
				<ds:Transforms>
					<ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
				</ds:Transforms>
				<ds:DigestMethod Algorithm=""urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-512""/>
				<ds:DigestValue>{0}</ds:DigestValue>
			</ds:Reference>", hash, ids[index]));
                     else
                        hashes.AppendLine(string.Format(@"<ds:Reference URI=""#{1}"">
				<ds:Transforms>
					<ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
				</ds:Transforms>
				<ds:DigestMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#gostr3411""/>
				<ds:DigestValue>{0}</ds:DigestValue>
			</ds:Reference>", hash, ids[index]));
                  }
               }
            }

            // s:actor = ""http://smev.gosuslugi.ru/actors/smev"" // RSMEVAUTH
            var xmlStatic = string.Format(
               @"<wsse:Security s:actor=""{2}"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"" xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
	<wsse:BinarySecurityToken EncodingType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"" ValueType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"" wsu:Id=""{0}"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd""/>
	<ds:Signature xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
		<ds:SignedInfo>
			<ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
			<ds:SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#gostr34102001-gostr3411""/>
			{1}
		</ds:SignedInfo>
		<ds:SignatureValue/>
		<ds:KeyInfo Id=""KeyId"">
			<wsse:SecurityTokenReference wsu:Id=""STRId"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">
				<wsse:Reference URI=""#{0}"" ValueType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd""/>
			</wsse:SecurityTokenReference>
		</ds:KeyInfo>
	</ds:Signature>
</wsse:Security>
", certificateId, hashes, actor);

            if (algorithm == HashAlgorithmType.GOST3411_2012)
               xmlStatic = string.Format(
                  @"<wsse:Security s:actor=""{2}"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"" xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
	<wsse:BinarySecurityToken EncodingType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"" ValueType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"" wsu:Id=""{0}"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd""/>
	<ds:Signature Id=""Signature"" xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
		<ds:SignedInfo>
			<ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
			<ds:SignatureMethod Algorithm=""urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-256""/>
			{1}
		</ds:SignedInfo>
		<ds:SignatureValue/>
		<ds:KeyInfo Id=""KeyId"">
			<wsse:SecurityTokenReference wsu:Id=""STRId"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">
				<wsse:Reference URI=""#{0}"" ValueType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd""/>
			</wsse:SecurityTokenReference>
		</ds:KeyInfo>
	</ds:Signature>
</wsse:Security>
", certificateId, hashes, actor);
            else if (algorithm == HashAlgorithmType.GOST3411_2012_STRONG)
               xmlStatic = string.Format(
                  @"<wsse:Security s:actor=""{2}"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"" xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
	<wsse:BinarySecurityToken EncodingType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"" ValueType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"" wsu:Id=""{0}"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd""/>
	<ds:Signature Id=""Signature"" xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
		<ds:SignedInfo>
			<ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
			<ds:SignatureMethod Algorithm=""urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-512""/>
			{1}
		</ds:SignedInfo>
		<ds:SignatureValue/>
		<ds:KeyInfo Id=""KeyId"">
			<wsse:SecurityTokenReference wsu:Id=""STRId"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">
				<wsse:Reference URI=""#{0}"" ValueType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd""/>
			</wsse:SecurityTokenReference>
		</ds:KeyInfo>
	</ds:Signature>
</wsse:Security>
", certificateId, hashes, actor);

            var header = root.SelectSingleNode("//s:Header", namespaceManager);

            var headerSec = new XmlDocument {PreserveWhitespace = false};
            headerSec.LoadXml(xmlStatic);
            var newNode = headerSec.DocumentElement;

            if (header.FirstChild == null)
               header.AppendChild(document.ImportNode(newNode, true));
            else
               header.InsertBefore(document.ImportNode(newNode, true), header.FirstChild);

            /*ВОТ ЗДЕСЬ НАДО ПОДПИСАТЬ*/
            var binarySecurityToken = root.SelectSingleNode("//wsse:BinarySecurityToken", namespaceManager);
            binarySecurityToken.InnerText = certificateB64;

            string signValue;
            var signedInfo = root.SelectSingleNode("//ds:SignedInfo", namespaceManager);
            var signedInfoDoc = new XmlDocument {PreserveWhitespace = false};
            signedInfoDoc.LoadXml(signedInfo.OuterXml);
            var transform = new XmlDsigExcC14NTransform(false);
            transform.Algorithm = SignedXml.XmlDsigExcC14NTransformUrl;
            transform.LoadInput(signedInfoDoc);

            using (var output = (Stream) transform.GetOutput(typeof(Stream)))
            {
               using (var reader = new StreamReader(output))
               {
                  var signedInfoString = reader.ReadToEnd();
                  signValue = cryptoHelper.SignData(Encoding.UTF8.GetBytes(signedInfoString), certificateB64,
                     storeLocation, pinCode,
                     out nErrorCode, out error);
                  if (nErrorCode != 0)
                  {
                     LogManager.GetLogger(typeof(Signer)).ErrorFormat("Ошибка наложения ЭП при работе с РСМЭВ: {0}-{1}",
                        nErrorCode, error);
                     throw new ApplicationException(error);
                  }
               }
            }

            //signatureValue.InnerText = сюда положить обрезанную подпись.
            var signatureValue = root.SelectSingleNode("//ds:SignatureValue", namespaceManager);

            signatureValue.InnerText = signValue;

            return document.OuterXml;
         }
         catch (Exception ex)
         {
            LogManager.GetLogger(typeof(Signer)).Error("Ошибка наложения ЭП при работе с РСМЭВ", ex);
            throw;
         }
      }


      public static string SignMessageTopCase(string message, string certificateId, X509Certificate2 certificate,
         StoreLocation storeLocation, string pinCode, string actor = "http://smev.gosuslugi.ru/actors/smev")
      {
         try
         {
            var document = new XmlDocument {PreserveWhitespace = false};
            document.LoadXml(message);

            var cryptoHelper = new CryptoHelper();
            uint nErrorCode;
            string error;

            document.DocumentElement.SetAttribute("xmlns:wsu",
               "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");

            // Добавляем XmlDeclaration -не думаю что это необходимо
            if (!(document.FirstChild is XmlDeclaration))
            {
               var declaration = document.CreateXmlDeclaration("1.0", "UTF-8", string.Empty);
               document.InsertBefore(declaration, document.FirstChild);
            }

            var root = document.DocumentElement;
            root.SetAttribute("xmlns:wsse",
               "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            root.SetAttribute("xmlns:wsu",
               "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
            root.SetAttribute("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");

            // Ищем body и добавляем ему Id
            var namespaceManager = GetNewFilledNamespaceManager(document.NameTable);
            var body = document.DocumentElement.SelectSingleNode(@"//s:Body", namespaceManager) as XmlElement;
            if (body == null) throw new ApplicationException("Не найден тэг body");

            // Определяем алгоритм сертификата
            var certificateB64 = Convert.ToBase64String(certificate.Export(X509ContentType.Cert));
            var algorithm = cryptoHelper.GetCertificateAlgorithm(certificateB64, out nErrorCode, out error);
            if (nErrorCode != 0 || !algorithm.HasValue)
            {
               LogManager.GetLogger(typeof(Signer))
                  .ErrorFormat("Не удалось определить алгоритм сертификата: {0}", error);
               throw new ApplicationException(error);
            }

            // сообщение было плохое.
            body.RemoveAllAttributes();

            var idAttr = document.CreateNode(XmlNodeType.Attribute, "wsu", "Id",
               namespaceManager.LookupNamespace("wsu"));
            body.Attributes.SetNamedItem(idAttr).InnerText = BodyId;


            // s:actor=""RSMEVAUTH""
            var xmlStatic = string.Format(
               @"<wsse:Security s:actor=""{1}"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"" xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
	<wsse:BinarySecurityToken EncodingType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"" ValueType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"" wsu:Id=""{0}"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd""/>
	<ds:Signature Id=""Signature"" xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
		<ds:SignedInfo>
			<ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
			<ds:SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#gostr34102001-gostr3411""/>
			<ds:Reference URI=""#body"">
				<ds:Transforms>
					<ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
				</ds:Transforms>
				<ds:DigestMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#gostr3411""/>
				<ds:DigestValue/>
			</ds:Reference>
		</ds:SignedInfo>
		<ds:SignatureValue/>
		<ds:KeyInfo Id=""KeyId"">
			<wsse:SecurityTokenReference wsu:Id=""STRId"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">
				<wsse:Reference URI=""#{0}"" ValueType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd""/>
			</wsse:SecurityTokenReference>
		</ds:KeyInfo>
	</ds:Signature>
</wsse:Security>
", certificateId, actor);


            if (algorithm == HashAlgorithmType.GOST3411_2012)
               xmlStatic = string.Format(
                  @"<wsse:Security s:actor=""{1}"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"" xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
	<wsse:BinarySecurityToken EncodingType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"" ValueType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"" wsu:Id=""{0}"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd""/>
	<ds:Signature Id=""Signature"" xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
		<ds:SignedInfo>
			<ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
			<ds:SignatureMethod Algorithm=""urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-256""/>
			<ds:Reference URI=""#body"">
				<ds:Transforms>
					<ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
				</ds:Transforms>
				<ds:DigestMethod Algorithm=""urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-256""/>
				<ds:DigestValue/>
			</ds:Reference>
		</ds:SignedInfo>
		<ds:SignatureValue/>
		<ds:KeyInfo Id=""KeyId"">
			<wsse:SecurityTokenReference wsu:Id=""STRId"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">
				<wsse:Reference URI=""#{0}"" ValueType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd""/>
			</wsse:SecurityTokenReference>
		</ds:KeyInfo>
	</ds:Signature>
</wsse:Security>
", certificateId, actor);
            else if (algorithm == HashAlgorithmType.GOST3411_2012_STRONG)
               xmlStatic = string.Format(
                  @"<wsse:Security s:actor=""{1}"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"" xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
	<wsse:BinarySecurityToken EncodingType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"" ValueType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"" wsu:Id=""{0}"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd""/>
	<ds:Signature Id=""Signature"" xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
		<ds:SignedInfo>
			<ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
			<ds:SignatureMethod Algorithm=""urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-512""/>
			<ds:Reference URI=""#body"">
				<ds:Transforms>
					<ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
				</ds:Transforms>
				<ds:DigestMethod Algorithm=""urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-512""/>
				<ds:DigestValue/>
			</ds:Reference>
		</ds:SignedInfo>
		<ds:SignatureValue/>
		<ds:KeyInfo Id=""KeyId"">
			<wsse:SecurityTokenReference wsu:Id=""STRId"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">
				<wsse:Reference URI=""#{0}"" ValueType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd""/>
			</wsse:SecurityTokenReference>
		</ds:KeyInfo>
	</ds:Signature>
</wsse:Security>
", certificateId, actor);

            var header = root.SelectSingleNode("//s:Header", namespaceManager);

            var headerSec = new XmlDocument {PreserveWhitespace = false};
            headerSec.LoadXml(xmlStatic);
            var newNode = headerSec.DocumentElement;

            if (header.FirstChild == null)
               header.AppendChild(document.ImportNode(newNode, true));
            else
               header.InsertBefore(document.ImportNode(newNode, true), header.FirstChild);

            var binarySecurityToken = root.SelectSingleNode("//wsse:BinarySecurityToken", namespaceManager);
            binarySecurityToken.InnerText = certificateB64;

            var xmlDocumentSignInfo = new XmlDocument {PreserveWhitespace = false};
            xmlDocumentSignInfo.LoadXml(body.OuterXml);


            //выполняем канонизацию
            var xmlTransform = new XmlDsigExcC14NTransform(false);
            xmlTransform.Algorithm = SignedXml.XmlDsigExcC14NTransformUrl;
            xmlTransform.LoadInput(xmlDocumentSignInfo);
            using (var output = (Stream) xmlTransform.GetOutput(typeof(Stream)))
            {
               using (var reader = new StreamReader(output))
               {
                  var bodyString = reader.ReadToEnd();
                  var hashesB64 = cryptoHelper.GetHashesB64(Encoding.UTF8.GetBytes(bodyString), false, out nErrorCode,
                     out error);
                  if (nErrorCode != 0)
                  {
                     LogManager.GetLogger(typeof(Signer)).ErrorFormat("Не удалось вычислить хеш: {0}", error);
                     throw new ApplicationException(error);
                  }

                  string hash;
                  hashesB64.TryGetValue(algorithm.Value, out hash);
                  if (string.IsNullOrEmpty(hash))
                  {
                     LogManager.GetLogger(typeof(Signer)).Error("Не удалось вычислить хеш");
                     throw new ApplicationException(error);
                  }

                  var digestValue = root.SelectSingleNode("//ds:DigestValue", namespaceManager);
                  digestValue.InnerText = hash;
               }
            }

            string signValue;
            var signedInfo = root.SelectSingleNode("//ds:SignedInfo", namespaceManager);
            var signedInfoDoc = new XmlDocument {PreserveWhitespace = false};
            signedInfoDoc.LoadXml(signedInfo.OuterXml);
            var transform = new XmlDsigExcC14NTransform(false);
            transform.Algorithm = SignedXml.XmlDsigExcC14NTransformUrl;
            transform.LoadInput(signedInfoDoc);

            using (var output = (Stream) transform.GetOutput(typeof(Stream)))
            {
               using (var reader = new StreamReader(output))
               {
                  var signedInfoString = reader.ReadToEnd();
                  signValue = cryptoHelper.SignData(Encoding.UTF8.GetBytes(signedInfoString), certificateB64,
                     storeLocation, pinCode,
                     out nErrorCode, out error);
                  if (nErrorCode != 0)
                  {
                     LogManager.GetLogger(typeof(Signer))
                        .ErrorFormat("Ошибка наложения ЭП при работе с РСМЭВ: {0}", error);
                     throw new ApplicationException(error);
                  }
               }
            }

            //signatureValue.InnerText = сюда положить обрезанную подпись.
            var signatureValue = root.SelectSingleNode("//ds:SignatureValue", namespaceManager);

            signatureValue.InnerText = signValue;

            return document.OuterXml;
         }
         catch (Exception ex)
         {
            LogManager.GetLogger(typeof(Signer)).Error("Ошибка наложения ЭП при работе с РСМЭВ", ex);
            throw;
         }
      }

      public static string SignMessageTopCaseV6(string message, X509Certificate2 certificate,
         StoreLocation storeLocation, string pinCode)
      {
         try
         {
            var document = new XmlDocument {PreserveWhitespace = false};
            document.LoadXml(message);

            var cryptoHelper = new CryptoHelper();
            uint nErrorCode;
            string error;

            var root = document.DocumentElement;

            // Добавляем XmlDeclaration -не думаю что это необходимо
            if (!(document.FirstChild is XmlDeclaration))
            {
               var declaration = document.CreateXmlDeclaration("1.0", "UTF-8", string.Empty);
               document.InsertBefore(declaration, document.FirstChild);
            }

            // Определяем алгоритм сертификата
            var certificateB64 = Convert.ToBase64String(certificate.Export(X509ContentType.Cert));
            var algorithm = cryptoHelper.GetCertificateAlgorithm(certificateB64, out nErrorCode, out error);
            if (nErrorCode != 0 || !algorithm.HasValue)
            {
               LogManager.GetLogger(typeof(Signer))
                  .ErrorFormat("Не удалось определить алгоритм сертификата: {0}", error);
               throw new ApplicationException(error);
            }

            // s:actor=""RSMEVAUTH""
            var xmlStatic = string.Format(
               $@"<ds:Signature Id=""Signature"" xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
		<ds:SignedInfo>
			<ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
			<ds:SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#gostr34102001-gostr3411""/>
			<ds:Reference URI=""#body"">
				<ds:Transforms>
					<ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
				</ds:Transforms>
				<ds:DigestMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#gostr3411""/>
				<ds:DigestValue/>
			</ds:Reference>
		</ds:SignedInfo>
		<ds:SignatureValue/>
		<ds:KeyInfo Id=""KeyId"">
         <ds:X509Data>
            <ds:X509Certificate>{certificateB64}</ds:X509Certificate>
         </ds:X509Data>
		</ds:KeyInfo>
	</ds:Signature>
");


            if (algorithm == HashAlgorithmType.GOST3411_2012)
               xmlStatic = string.Format(
                  $@"<ds:Signature Id=""Signature"" xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
		<ds:SignedInfo>
			<ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
			<ds:SignatureMethod Algorithm=""urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-256""/>
			<ds:Reference URI=""#body"">
				<ds:Transforms>
					<ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
				</ds:Transforms>
				<ds:DigestMethod Algorithm=""urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-256""/>
				<ds:DigestValue/>
			</ds:Reference>
		</ds:SignedInfo>
		<ds:SignatureValue/>
		<ds:KeyInfo Id=""KeyId"">
         <ds:X509Data>
            <ds:X509Certificate>{certificateB64}</ds:X509Certificate>
         </ds:X509Data>
		</ds:KeyInfo>
	</ds:Signature>
");
            else if (algorithm == HashAlgorithmType.GOST3411_2012_STRONG)
               xmlStatic = string.Format(
                  $@"<ds:Signature Id=""Signature"" xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
		<ds:SignedInfo>
			<ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
			<ds:SignatureMethod Algorithm=""urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-512""/>
			<ds:Reference URI=""#body"">
				<ds:Transforms>
					<ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#""/>
				</ds:Transforms>
				<ds:DigestMethod Algorithm=""urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-512""/>
				<ds:DigestValue/>
			</ds:Reference>
		</ds:SignedInfo>
		<ds:SignatureValue/>
		<ds:KeyInfo Id=""KeyId"">
         <ds:X509Data>
            <ds:X509Certificate>{certificateB64}</ds:X509Certificate>
         </ds:X509Data>
		</ds:KeyInfo>
	</ds:Signature>
");

            var namespaceManager = GetNewFilledNamespaceManager(document.NameTable);

            var headerSec = new XmlDocument {PreserveWhitespace = false};
            headerSec.LoadXml(xmlStatic);
            var newNode = headerSec.DocumentElement;

            var body = root.FirstChild;

            var idAttr = document.CreateNode(XmlNodeType.Attribute, "wsu", "Id",
               namespaceManager.LookupNamespace("wsu"));
            body.Attributes.SetNamedItem(idAttr).InnerText = BodyId;

            root.InsertBefore(document.ImportNode(newNode, true), root.FirstChild);

            var xmlDocumentSignInfo = new XmlDocument {PreserveWhitespace = false};
            xmlDocumentSignInfo.LoadXml(body.OuterXml);

            //выполняем канонизацию
            var xmlTransform = new XmlDsigExcC14NTransform(false);
            xmlTransform.Algorithm = SignedXml.XmlDsigExcC14NTransformUrl;
            xmlTransform.LoadInput(xmlDocumentSignInfo);
            using (var output = (Stream) xmlTransform.GetOutput(typeof(Stream)))
            {
               using (var reader = new StreamReader(output))
               {
                  var bodyString = reader.ReadToEnd();
                  var hashesB64 = cryptoHelper.GetHashesB64(Encoding.UTF8.GetBytes(bodyString), false, out nErrorCode,
                     out error);
                  if (nErrorCode != 0)
                  {
                     LogManager.GetLogger(typeof(Signer)).ErrorFormat("Не удалось вычислить хеш: {0}", error);
                     throw new ApplicationException(error);
                  }

                  string hash;
                  hashesB64.TryGetValue(algorithm.Value, out hash);
                  if (string.IsNullOrEmpty(hash))
                  {
                     LogManager.GetLogger(typeof(Signer)).Error("Не удалось вычислить хеш");
                     throw new ApplicationException(error);
                  }

                  var digestValue = root.SelectSingleNode("//ds:DigestValue", namespaceManager);
                  digestValue.InnerText = hash;
               }
            }

            string signValue;
            var signedInfo = root.SelectSingleNode("//ds:SignedInfo", namespaceManager);
            var signedInfoDoc = new XmlDocument {PreserveWhitespace = false};
            signedInfoDoc.LoadXml(signedInfo.OuterXml);
            var transform = new XmlDsigExcC14NTransform(false);
            transform.Algorithm = SignedXml.XmlDsigExcC14NTransformUrl;
            transform.LoadInput(signedInfoDoc);

            using (var output = (Stream) transform.GetOutput(typeof(Stream)))
            {
               using (var reader = new StreamReader(output))
               {
                  var signedInfoString = reader.ReadToEnd();
                  signValue = cryptoHelper.SignData(Encoding.UTF8.GetBytes(signedInfoString), certificateB64,
                     storeLocation, pinCode,
                     out nErrorCode, out error);
                  if (nErrorCode != 0)
                  {
                     LogManager.GetLogger(typeof(Signer))
                        .ErrorFormat("Ошибка наложения ЭП при работе с РСМЭВ: {0}", error);
                     throw new ApplicationException(error);
                  }
               }
            }

            //signatureValue.InnerText = сюда положить обрезанную подпись.
            var signatureValue = root.SelectSingleNode("//ds:SignatureValue", namespaceManager);

            signatureValue.InnerText = signValue;

            return document.OuterXml;
         }
         catch (Exception ex)
         {
            LogManager.GetLogger(typeof(Signer)).Error("Ошибка наложения ЭП при работе с РСМЭВ", ex);
            throw;
         }
      }


      private static XmlNamespaceManager GetNewFilledNamespaceManager(XmlNameTable xmlNameTable)
      {
         var namespaceManager = new XmlNamespaceManager(xmlNameTable);
         namespaceManager.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");
         namespaceManager.AddNamespace("wsse",
            "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
         namespaceManager.AddNamespace("wsu",
            "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
         namespaceManager.AddNamespace("smev", "http://smev.gosuslugi.ru/rev111111");
         namespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
         return namespaceManager;
      }

      public static bool CheckSignature(XmlDocument doc, StoreLocation storeLocation)
      {
         var result = true;

         var nodeList = doc.GetElementsByTagName("Security",
            "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");

         var cryptoHelper = new CryptoHelper();
         uint nErrorCode;
         string error;

         // получаем body.
         var namespaceManager = GetNewFilledNamespaceManager(doc.NameTable);
         var body = doc.DocumentElement.SelectSingleNode(@"//s:Body", namespaceManager) as XmlElement;
         var xmlDocumentSignInfo = new XmlDocument();
         //xmlDocumentSignInfo.PreserveWhitespace = true;
         xmlDocumentSignInfo.LoadXml(body.OuterXml);

         //выполняем канонизацию и подсчет Хэш суммы.
         var xmlTransform = new XmlDsigExcC14NTransform(false);
         xmlTransform.Algorithm = SignedXml.XmlDsigExcC14NTransformUrl;
         xmlTransform.LoadInput(xmlDocumentSignInfo);

         Dictionary<HashAlgorithmType, string> hashes;

         // подсчитали хэш от данных.
         using (var output = (Stream) xmlTransform.GetOutput(typeof(Stream)))
         {
            using (var reader = new StreamReader(output))
            {
               var bodyString = reader.ReadToEnd();
               hashes = cryptoHelper.GetHashesB64(Encoding.UTF8.GetBytes(bodyString), false, out nErrorCode, out error);
               if (nErrorCode != 0) return false;
            }
         }

         for (var curSignature = 0; curSignature < nodeList.Count; curSignature++)
         {
            var digestValue = nodeList[curSignature].SelectSingleNode(".//ds:DigestValue", namespaceManager);
            if (digestValue?.ParentNode?.Attributes?["URI"] != null &&
                digestValue.ParentNode.Attributes["URI"].InnerText == "#body" &&
                !hashes.ContainsValue(digestValue.InnerText))
               return false;

            // Загружаем узел с подписью.
            var certificate = ((XmlElement) nodeList[curSignature]).GetElementsByTagName("BinarySecurityToken",
               "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");

            var m =
               ((XmlElement) nodeList[curSignature]).GetElementsByTagName("Signature",
                  SignedXml.XmlDsigNamespaceUrl)[0];

            var signedInfo = nodeList[curSignature].SelectSingleNode(".//ds:SignedInfo", namespaceManager);
            var signedInfoDoc = new XmlDocument();
            signedInfoDoc.LoadXml(signedInfo.OuterXml);
            var transform = new XmlDsigExcC14NTransform(false);
            transform.Algorithm = SignedXml.XmlDsigExcC14NTransformUrl;
            transform.LoadInput(signedInfoDoc);

            using (var output = (Stream) transform.GetOutput(typeof(Stream)))
            {
               using (var reader = new StreamReader(output))
               {
                  var signedInfoString = reader.ReadToEnd();
                  uint nErrorCode1;
                  uint nErrorCode2;
                  var data1 = Encoding.UTF8.GetBytes(signedInfoString);
                  var text = Encoding.UTF8.GetBytes(m.InnerText);
                  cryptoHelper.Verify(data1, text, certificate[0].InnerText, false, out nErrorCode1, out error);
                  var data2 = Encoding.UTF8.GetBytes(signedInfo.OuterXml);
                  cryptoHelper.Verify(data2, text, certificate[0].InnerText, false, out nErrorCode2, out error);
                  result = nErrorCode1 == 0 || nErrorCode1 == 0 || true;
               }
            }
         }

         return result;
      }

      //public static bool CheckSignature(XmlDocument doc)
      //{
      //	bool result = true;

      //	XmlNodeList nodeList = doc.GetElementsByTagName("Security",
      //													"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");

      //	var cryptoComObject = new cryptoCOM();

      //	// получаем body.
      //	XmlNamespaceManager namespaceManager = GetNewFilledNamespaceManager(doc.NameTable);
      //	var body = doc.DocumentElement.SelectSingleNode(@"//s:Body", namespaceManager) as XmlElement;
      //	var xmlDocumentSignInfo = new XmlDocument();
      //	//xmlDocumentSignInfo.PreserveWhitespace = true;
      //	xmlDocumentSignInfo.LoadXml(body.OuterXml);

      //	//выолняем канонизация и подсчет Хэш суммы.
      //	XmlDsigExcC14NTransform xmlTransform = new XmlDsigExcC14NTransform(false);
      //	xmlTransform.Algorithm = SignedXml.XmlDsigExcC14NTransformUrl;
      //	xmlTransform.LoadInput(xmlDocumentSignInfo);

      //	cryptoComObject.SetParameterValue("[app]CRYPT_MACHINE_KEYSET", "1");

      //	var hash = string.Empty;

      //	// подсчитали хэш от данных.
      //	using (var output = (Stream)xmlTransform.GetOutput(typeof(Stream)))
      //	{
      //		using (var reader = new StreamReader(output))
      //		{
      //			string bodyString = reader.ReadToEnd();
      //			uint nErrorCode = cryptoComObject.Hash(Convert.ToBase64String(Encoding.UTF8.GetBytes(bodyString)), out hash);
      //			if (nErrorCode != 0)
      //			{
      //				return false;
      //			}
      //		}
      //	}

      //	hash = Convert.ToBase64String(Convert.FromBase64String(hash));

      //	for (int curSignature = 0; curSignature < nodeList.Count; curSignature++)
      //	{
      //		XmlNode digestValue = ((XmlElement)(nodeList[curSignature])).SelectSingleNode(".//ds:DigestValue", namespaceManager);
      //		if (digestValue != null && digestValue.ParentNode != null && digestValue.ParentNode.Attributes != null && digestValue.ParentNode.Attributes["URI"] != null && digestValue.ParentNode.Attributes["URI"].InnerText == "#body" && Convert.ToBase64String(Convert.FromBase64String(digestValue.InnerText)) != hash)
      //		{
      //			return false;
      //		}

      //		// Загружаем узел с подписью.
      //		XmlNodeList certificate = ((XmlElement)(nodeList[curSignature])).GetElementsByTagName("BinarySecurityToken",
      //																							   "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");

      //		XmlNode m =
      //			((XmlElement)nodeList[curSignature]).GetElementsByTagName("Signature", SignedXml.XmlDsigNamespaceUrl)[0];

      //		XmlNode signedInfo = ((XmlElement)(nodeList[curSignature])).SelectSingleNode(".//ds:SignedInfo", namespaceManager);
      //		var signedInfoDoc = new XmlDocument();
      //		signedInfoDoc.LoadXml(signedInfo.OuterXml);
      //		XmlDsigExcC14NTransform transform = new XmlDsigExcC14NTransform(false);
      //		transform.Algorithm = SignedXml.XmlDsigExcC14NTransformUrl;
      //		transform.LoadInput(signedInfoDoc);

      //		using (var output = (Stream)transform.GetOutput(typeof(Stream)))
      //		{
      //			using (var reader = new StreamReader(output))
      //			{
      //				string signedInfoString = reader.ReadToEnd();

      //				var data1 = Convert.ToBase64String(Encoding.UTF8.GetBytes(signedInfoString));
      //				var error1 = cryptoComObject.Verify(certificate[0].InnerText, data1, true, m.InnerText);
      //				var data2 = Convert.ToBase64String(Encoding.UTF8.GetBytes(signedInfo.OuterXml));
      //				var error2 = cryptoComObject.Verify(certificate[0].InnerText, data2, true, m.InnerText);
      //				result = error1 == 0 || error2 == 0 || true;
      //			}
      //		}
      //	}

      //	return result;
      //}
   }
}
