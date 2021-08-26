using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;
using UtilsSmev.Interface;
using log4net;

namespace UtilsSmev.Inspectors
{
	public class SmevServiceInspector : BehaviorExtensionElement, IServiceBehavior, IDispatchMessageInspector
	{
		private const string LoggingInstanceTypeName = "loggingInstanceType";
		private const string XpathsStringsName = "XpathsStrings";
		private const string ActorName = "Actor";
		private const string IdsStringsName = "IdsStrings";
		private const string NamespasesStringsName = "NamespasesStrings";
		private const string PrefixStringsName = "PrefixStrings";
		private const string CustomWsdlPathName = "CustomWsdlPath";
      private const string ThumbprintInConfigName = "SmevCertThumbprint";

      private readonly string _pinCode = ConfigurationManager.AppSettings["serverCertPinCode"] ?? string.Empty;
		private readonly X509Certificate2 _serviceCert;
		private ILoggingSmevRequest _logging;

      private string Thumbprint => ConfigurationManager.AppSettings[ThumbprintInConfigName];
      private readonly StoreLocation _storeLocation = (StoreLocation)Enum.Parse(typeof(StoreLocation), ConfigurationManager.AppSettings["clientCertStoreLocation"], true);


      public SmevServiceInspector()
		{
			object serverCertThumbprint = ConfigurationManager.AppSettings["serverCert"];
			var findType =
				(X509FindType) Enum.Parse(typeof (X509FindType), ConfigurationManager.AppSettings["serverCertFindType"], true);
			var storeName =
				(StoreName) Enum.Parse(typeof (StoreName), ConfigurationManager.AppSettings["serverCertStoreName"], true);
			var storeLocation =
				(StoreLocation)
				Enum.Parse(typeof (StoreLocation), ConfigurationManager.AppSettings["serverCertStoreLocation"], true);
			var store = new X509Store(storeName, storeLocation);
			store.Open(OpenFlags.ReadOnly);

			//сертификат
			X509Certificate2Collection coll = store.Certificates.Find(findType, serverCertThumbprint, true);

			if (coll.Count == 0)
			{
				var sb = new StringBuilder();
				for (int i = 0; i < store.Certificates.Count; i++)
				{
					string a = store.Certificates[i].Thumbprint.ToUpper();
					string b = serverCertThumbprint.ToString().Trim().Replace(" ", string.Empty).ToUpper();


					if (a.Contains(b) || b.Contains(a))
					{
						_serviceCert = store.Certificates[i];
						return;
					}
					sb.AppendFormat("{0} '{1}'='{2}'", a.Contains(b) || b.Contains(a), a, b);
					sb.AppendLine(store.Certificates[i].Thumbprint);
				}

				if (_serviceCert == null)
				{
					LogManager.GetLogger(typeof (SmevServiceInspector))
					          .ErrorFormat("Сертификат сервера не найден. Отпечаток {0}; Сертификаты в хранилище: {1}",
					                       serverCertThumbprint, sb);
					return;
				}
			}

			_serviceCert = coll[0];
		}

		public SmevServiceInspector(X509Certificate2 certificate, string pinCode)
		{
			_serviceCert = certificate;
			_pinCode = pinCode;
		}

		public List<string> Xpaths
		{
			get
			{
				return string.IsNullOrEmpty(XpathsStrings)
					       ? new List<string>()
					       : XpathsStrings.Split('|').Where(s => !string.IsNullOrEmpty(s)).ToList();
			}
		}

		public List<string> Ids
		{
			get
			{
				return string.IsNullOrEmpty(IdsStrings)
					       ? new List<string>()
					       : IdsStrings.Split('|').Where(s => !string.IsNullOrEmpty(s)).ToList();
			}
		}

		public List<string> Namespases
		{
			get
			{
				return string.IsNullOrEmpty(NamespasesStrings)
					       ? new List<string>()
					       : NamespasesStrings.Split('|').Where(s => !string.IsNullOrEmpty(s)).ToList();
			}
		}

		public List<string> Prefix
		{
			get
			{
				return string.IsNullOrEmpty(PrefixStrings)
					       ? new List<string>()
					       : PrefixStrings.Split('|').Where(s => !string.IsNullOrEmpty(s)).ToList();
			}
		}

		[ConfigurationProperty(XpathsStringsName, IsRequired = false)]
		public string XpathsStrings
		{
			get { return (string) base[XpathsStringsName]; }

			set { base[XpathsStringsName] = value; }
		}

		[ConfigurationProperty(ActorName, IsRequired = false)]
		public string Actor
		{
			get { return String.IsNullOrWhiteSpace((string)base[ActorName]) ? "RSMEVAUTH" : (string)base[ActorName]; }

			set { base[ActorName] = value; }
		}

		[ConfigurationProperty(CustomWsdlPathName, IsRequired = false)]
		public string CustomWsdlPath
		{
			get { return (string)base[CustomWsdlPathName]; }

			set { base[CustomWsdlPathName] = value; }
		}

		[ConfigurationProperty(IdsStringsName, IsRequired = false)]
		public string IdsStrings
		{
			get { return (string) base[IdsStringsName]; }

			set { base[IdsStringsName] = value; }
		}

		[ConfigurationProperty(NamespasesStringsName, IsRequired = false)]
		public string NamespasesStrings
		{
			get { return (string) base[NamespasesStringsName]; }

			set { base[NamespasesStringsName] = value; }
		}

		[ConfigurationProperty(PrefixStringsName, IsRequired = false)]
		public string PrefixStrings
		{
			get { return (string) base[PrefixStringsName]; }

			set { base[PrefixStringsName] = value; }
		}

		[ConfigurationProperty(LoggingInstanceTypeName, IsRequired = false)]
		public string LoggingInstanceType
		{
			get { return (string) base[LoggingInstanceTypeName]; }

			set { base[LoggingInstanceTypeName] = value; }
		}

		private ILoggingSmevRequest Logging
		{
			get
			{
				if (_logging == null)
				{
					if (!string.IsNullOrEmpty(LoggingInstanceType))
					{
						_logging = GetBusinessEntityType(LoggingInstanceType) as ILoggingSmevRequest;
					}
				}

				return _logging;
			}
		}

		public override Type BehaviorType
		{
			get { return typeof (SmevServiceInspector); }
		}

		public virtual object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			try
			{
				var xml = new XmlDocument();
				xml.LoadXml(request.ToString());

				if (!xml.DocumentElement.Name.EndsWith("Envelope"))
				{
					return null;
				}
			}
			catch
			{
				return null;
			}

			if (Logging != null)
			{
				Logging.SaveMessage(true, request.ToString(), OperationContext.Current.IncomingMessageHeaders.Action);
			}

			/*try
			{*/
				ValidateMessage(ref request);

            //Авторизация запроса (проверка тумпринта сертификата)
            ///AuthMessage(ref request);

				var req = request.ToString().Replace(@"mustUnderstand=""1""", @"mustUnderstand=""0""");
				var ms = new MemoryStream(Encoding.UTF8.GetBytes(req));
				XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(ms, new XmlDictionaryReaderQuotas());
				request = Message.CreateMessage(reader, Int32.MaxValue, request.Version);
			/*}
			catch(Exception ex)
			{
				LogManager.GetLogger(GetType()).Error("Ошибка при получении запроса РСМЭВ", ex);
			}*/

			return null;
		}

      /// <summary>
      /// Авторизация запроса (проверка тумпринта сертификата)
      /// </summary>
      /// <param name="request"></param>
      private void AuthMessage(ref Message request)
      {
         if (string.IsNullOrWhiteSpace(request.ToString()))
         {
            throw new ArgumentNullException(nameof(request));
         }

         var xml = new XmlDocument();
         xml.LoadXml(request.ToString());
         XmlNodeList SecurityTokens = xml.GetElementsByTagName("wsse:BinarySecurityToken");

         if (SecurityTokens.Count != 1)
            throw new MethodAccessException("SecurityToken not found");

         string securityTokenString = SecurityTokens[0].InnerText;
         if (string.IsNullOrWhiteSpace(securityTokenString))
            throw new MethodAccessException("SecurityToken not found");

         byte[] _certPre = Convert.FromBase64String(securityTokenString);

         X509Certificate2 cert = new X509Certificate2(_certPre);

         if(!string.Equals(cert.Thumbprint, Thumbprint, StringComparison.Ordinal))
            throw new MethodAccessException("SecurityToken not found");
      }

      public virtual void BeforeSendReply(ref Message reply, object correlationState)
		{
			try
			{
				var xml = new XmlDocument();
				xml.LoadXml(reply.ToString());

				if (!xml.DocumentElement.Name.EndsWith("Envelope"))
				{
					if (!String.IsNullOrWhiteSpace(CustomWsdlPath) && WsdlTuner.IsWsdl(reply.ToString()))
						reply = WsdlTuner.CustomWsdl(reply.Version, CustomWsdlPath);
					return;
				}
			}
			catch
			{
				return;
			}

			string outgoingMessage = reply.ToString();
			string signedSoapMessage = Xpaths != null && Xpaths.Count > 0
					                        ? Signer.SignMessageTopCase(outgoingMessage, "ep-ov", _serviceCert, _storeLocation, _pinCode, Xpaths,
					                                                    Ids,
					                                                    Namespases, Prefix, Actor)
					                        : Signer.SignMessageTopCase(outgoingMessage, "ep-ov", _serviceCert, _storeLocation, _pinCode, Actor);
			if (Logging != null)
			{
				Logging.SaveMessage(false, signedSoapMessage, OperationContext.Current.IncomingMessageHeaders.Action);
			}

			var ms = new MemoryStream(Encoding.UTF8.GetBytes(signedSoapMessage));
			XmlReader reader = XmlReader.Create(ms);
			reply = Message.CreateMessage(reader, Int32.MaxValue, reply.Version);
		}

		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			//Пусто
		}

		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
		                                 Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
			//Пусто
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcher channel
				in serviceHostBase.ChannelDispatchers)
			{
				foreach (EndpointDispatcher endpoing
					in channel.Endpoints)
				{
					endpoing
						.DispatchRuntime
						.MessageInspectors
						.Add(this);
				}
			}
		}

		private static object GetBusinessEntityType(string typeName)
		{
			List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

			foreach (Assembly assembly in assemblies)
			{
				Type t = assembly.GetType(typeName, false);
				if (t != null)
					return Activator.CreateInstance(t);
			}

			throw new ArgumentException("Type " + typeName + " doesn't exist in the current app domain");
		}

		private void ValidateMessage(ref Message request)
		{
			if (string.IsNullOrEmpty(request.ToString()))
			{
				return;
			}

			var xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(request.ToString());
			XmlNodeList faultElements = xmlDocument.GetElementsByTagName("fault");
			if (faultElements.Count > 0)
			{
				XmlNode faultElement = faultElements[0];
				string faultMessage = faultElement.Attributes["faultMessage"].Value;
				string faultCode = faultElement.Attributes["faultCode"].Value;
				throw new FaultException(faultMessage, new FaultCode(faultCode, "http://smev.gosuslugi.ru/rev120315"));
			}
		}

		protected override object CreateBehavior()
		{
			return new SmevServiceInspector
				{
					LoggingInstanceType = LoggingInstanceType,
					IdsStrings = IdsStrings,
					XpathsStrings = XpathsStrings,
					NamespasesStrings = NamespasesStrings,
					PrefixStrings = PrefixStrings,
					Actor = Actor,
					CustomWsdlPath = CustomWsdlPath
				};
		}
	}
}
