using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TopCasex64Lib;

namespace UtilsSmev.Crypto
{
   public class CryptoHelper
   {
      private static readonly List<HashAlgorithmType> HashTypes =
        Enum.GetValues(typeof(HashAlgorithmType)).Cast<HashAlgorithmType>().ToList();

      private readonly cryptoCOM _crypto;
      private readonly CryptoComEx _cryptoV2;

      public CryptoHelper()
      {
         _crypto = new cryptoCOM();
         _cryptoV2 = new CryptoComEx();
      }

      #region New interface (All algorithms)

      #region GetHashAlgorithmsSupported

      public List<HashAlgorithmType> GetHashAlgorithmsSupported()
      {
         var result = new List<HashAlgorithmType>();
         foreach (var hashType in HashTypes)
         {
            long handle;
            string hash;
            var nErrorCode = _cryptoV2.HashBegin((uint)hashType, out handle);
            if (nErrorCode == 0)
            {
               result.Add(hashType);
               _cryptoV2.HashEnd(ref handle, out hash);
            }
         }

         return result;
      }

      #endregion

      #region GetHashV2

      public byte[] GetHashV2(byte[] data, HashAlgorithmType hashType, bool stepMode, out uint nErrorCode, out string error)
      {
         var hash = GetHashV2B64(data, hashType, stepMode, out nErrorCode, out error);
         if (hash == null) return null;
         return Convert.FromBase64String(hash);
      }

      public string GetHashV2B64(byte[] data, HashAlgorithmType hashType, bool stepMode, out uint nErrorCode, out string error)
      {
         string hash;

         if (stepMode)
         {
            long handle;
            nErrorCode = _cryptoV2.HashBegin((uint)hashType, out handle);
            if (nErrorCode != 0)
            {
               error = _crypto.GetErrorDescription(nErrorCode);
               return null;
            }

            var bodyOffset = 0;
            var length = 1000000;
            var buffer = new byte[length];
            while (bodyOffset < data.Length)
            {
               length = Math.Min(length, data.Length - bodyOffset);
               if (length < buffer.Length)
               {
                  var buffer2 = new byte[length];
                  Buffer.BlockCopy(data, bodyOffset, buffer2, 0, length);
                  nErrorCode = _cryptoV2.HashUpdate(buffer2, handle);
               }
               else
               {
                  Buffer.BlockCopy(data, bodyOffset, buffer, 0, length);
                  nErrorCode = _cryptoV2.HashUpdate(buffer, handle);
               }

               if (nErrorCode != 0)
               {
                  error = _crypto.GetErrorDescription(nErrorCode);
                  _cryptoV2.HashEnd(handle, out hash);
                  return null;
               }

               bodyOffset += length;
            }

            nErrorCode = _cryptoV2.HashEnd(handle, out hash);
            if (nErrorCode != 0)
            {
               error = _crypto.GetErrorDescription(nErrorCode);
               return null;
            }
         }
         else
         {
            nErrorCode = _cryptoV2.Hash(data, (uint)hashType, out hash);
            if (nErrorCode != 0)
            {
               error = _crypto.GetErrorDescription(nErrorCode);
               return null;
            }
         }

         error = string.Empty;
         return hash;
      }

      public byte[] GetHashV2(string data, HashAlgorithmType hashType, out uint nErrorCode, out string error)
      {
         var hash = GetHashV2B64(data, hashType, out nErrorCode, out error);
         if (hash == null) return null;
         return Convert.FromBase64String(hash);
      }

      public string GetHashV2B64(string data, HashAlgorithmType hashType, out uint nErrorCode, out string error)
      {
         string hash;
         nErrorCode = _cryptoV2.Hash(data, (uint)hashType, out hash);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return hash;
      }

      #endregion

      #region GetHashes

      public Dictionary<HashAlgorithmType, byte[]> GetHashes(byte[] data, bool stepMode, out uint nErrorCode,
        out string error)
      {
         var hashes = GetHashesB64(data, stepMode, out nErrorCode, out error);
         return hashes?.ToDictionary(p => p.Key, p => Convert.FromBase64String(p.Value));
      }

      public Dictionary<HashAlgorithmType, string> GetHashesB64(byte[] data, bool stepMode, out uint nErrorCode,
        out string error)
      {
         var result = new Dictionary<HashAlgorithmType, string>();
         string hash;

         if (stepMode)
         {
            var handles = new Dictionary<HashAlgorithmType, long>();
            foreach (var hashType in HashTypes)
            {
               long handle;
               nErrorCode = _cryptoV2.HashBegin((uint)hashType, out handle);
               if (nErrorCode != 0)
               {
                  if (nErrorCode == CryptoErrorCodes.WinApi.CryptEUnknownAlgo &&
                      (hashType.GetAttribute<AlgorithmPropertiesAttribute>()?.Properties ?? AlgorithmProperties.Default)
                      .HasFlag(AlgorithmProperties.Optional)) continue;

                  error = _crypto.GetErrorDescription(nErrorCode);
                  foreach (var handle2 in handles.Values) _cryptoV2.HashEnd(handle2, out hash);
                  return null;
               }

               handles.Add(hashType, handle);
            }

            var bodyOffset = 0;
            var length = 1000000;
            var buffer = new byte[length];
            while (bodyOffset < data.Length)
            {
               length = Math.Min(length, data.Length - bodyOffset);
               if (length < buffer.Length)
               {
                  var buffer2 = new byte[length];
                  Buffer.BlockCopy(data, bodyOffset, buffer2, 0, length);
                  foreach (var handle in handles.Values)
                  {
                     nErrorCode = _cryptoV2.HashUpdate(buffer2, handle);
                     if (nErrorCode != 0)
                     {
                        error = _crypto.GetErrorDescription(nErrorCode);
                        foreach (var handle2 in handles.Values) _cryptoV2.HashEnd(handle2, out hash);
                        return null;
                     }
                  }
               }
               else
               {
                  Buffer.BlockCopy(data, bodyOffset, buffer, 0, length);
                  foreach (var handle in handles.Values)
                  {
                     nErrorCode = _cryptoV2.HashUpdate(buffer, handle);
                     if (nErrorCode != 0)
                     {
                        error = _crypto.GetErrorDescription(nErrorCode);
                        foreach (var handle2 in handles.Values) _cryptoV2.HashEnd(handle2, out hash);
                        return null;
                     }
                  }
               }

               bodyOffset += length;
            }

            foreach (var hashType in HashTypes.Where(p => handles.Keys.Contains(p)))
            {
               nErrorCode = _cryptoV2.HashEnd(handles[hashType], out hash);
               if (nErrorCode != 0)
               {
                  error = _crypto.GetErrorDescription(nErrorCode);
                  foreach (var handle in handles.Values.Where(p => p != handles[hashType]))
                     _cryptoV2.HashEnd(handle, out hash);
                  return null;
               }

               result.Add(hashType, hash);
               handles.Remove(hashType);
            }
         }
         else
         {
            foreach (var hashType in HashTypes)
            {
               nErrorCode = _cryptoV2.Hash(data, (uint)hashType, out hash);
               if (nErrorCode != 0)
               {
                  if (nErrorCode == CryptoErrorCodes.WinApi.CryptEUnknownAlgo &&
                      (hashType.GetAttribute<AlgorithmPropertiesAttribute>()?.Properties ?? AlgorithmProperties.Default)
                      .HasFlag(AlgorithmProperties.Optional)) continue;

                  error = _crypto.GetErrorDescription(nErrorCode);
                  return null;
               }

               result.Add(hashType, hash);
            }
         }

         nErrorCode = 0;
         error = string.Empty;
         return result;
      }

      public Dictionary<HashAlgorithmType, byte[]> GetHashes(string data, out uint nErrorCode, out string error)
      {
         var hashes = GetHashesB64(data, out nErrorCode, out error);
         return hashes?.ToDictionary(p => p.Key, p => Convert.FromBase64String(p.Value));
      }

      public Dictionary<HashAlgorithmType, string> GetHashesB64(string data, out uint nErrorCode, out string error)
      {
         var result = new Dictionary<HashAlgorithmType, string>();
         foreach (var hashType in HashTypes)
         {
            string hash;
            nErrorCode = _cryptoV2.Hash(Convert.FromBase64String(data), (uint)hashType, out hash);
            if (nErrorCode != 0)
            {
               if (nErrorCode == CryptoErrorCodes.WinApi.CryptEUnknownAlgo &&
                   (hashType.GetAttribute<AlgorithmPropertiesAttribute>()?.Properties ?? AlgorithmProperties.Default)
                   .HasFlag(AlgorithmProperties.Optional)) continue;

               error = _crypto.GetErrorDescription(nErrorCode);
               return null;
            }

            result.Add(hashType, hash);
         }

         nErrorCode = 0;
         error = string.Empty;
         return result;
      }

      #endregion

      #region SignData

      /// <summary>
      ///   «Голая» подпись
      /// </summary>
      /// <param name="data"></param>
      /// <param name="certificateB64"></param>
      /// <param name="location"></param>
      /// <param name="pinCode"></param>
      /// <param name="nErrorCode"></param>
      /// <param name="error"></param>
      /// <returns></returns>
      public string SignData(byte[] data, string certificateB64, StoreLocation? location, string pinCode,
        out uint nErrorCode, out string error)
      {
         if (location.HasValue)
            _crypto.SetParameterValue("[app]CRYPT_MACHINE_KEYSET",
              location.Value == StoreLocation.CurrentUser ? "0" : "1");
         if (pinCode != null) _crypto.SetPINCode(pinCode);
         string signature;
         nErrorCode = _cryptoV2.SignData(certificateB64, data, out signature);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return signature;
      }

      #endregion

      #region SignHashPkcs7

      /// <summary>
      ///   Pkcs7 подпись
      /// </summary>
      /// <param name="hash"></param>
      /// <param name="certificateB64"></param>
      /// <param name="pinCode"></param>
      /// <param name="location"></param>
      /// <param name="docDescription"></param>
      /// <param name="nErrorCode"></param>
      /// <param name="error"></param>
      /// <param name="signatureFormat"></param>
      /// <param name="tspUrl"></param>
      /// <param name="attributes"></param>
      /// <param name="docName"></param>
      /// <returns></returns>
      public byte[] SignHashPkcs7(string hash, string certificateB64, string pinCode, StoreLocation? location,
        SignAlgorithmType signatureFormat, string tspUrl, string attributes, string docName, string docDescription,
        out uint nErrorCode, out string error)
      {
         if (location.HasValue)
            _crypto.SetParameterValue("[app]CRYPT_MACHINE_KEYSET",
              location.Value == StoreLocation.CurrentUser ? "0" : "1");
         if (pinCode != null) _crypto.SetPINCode(pinCode);
         object result;
         nErrorCode = _cryptoV2.pkcs7Sign(certificateB64, hash, (uint)signatureFormat, tspUrl, attributes, docName,
           docDescription, out result);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return (byte[])result;
      }

      #endregion

      #region Verify

      /// <summary>
      ///   Проверка «голой» подписи
      /// </summary>
      /// <param name="data"></param>
      /// <param name="signature"></param>
      /// <param name="certificateB64"></param>
      /// <param name="isDataHash"></param>
      /// <param name="nErrorCode"></param>
      /// <param name="error"></param>
      /// <returns></returns>
      public bool Verify(byte[] data, byte[] signature, string certificateB64, bool isDataHash, out uint nErrorCode,
        out string error)
      {
         //_cryptoComObject.SetParameterValue("[app]CRYPT_MACHINE_KEYSET", location == StoreLocation.CurrentUser ? "0" : "1");
         nErrorCode = isDataHash
           ? _cryptoV2.VerifyHash(certificateB64, data, signature)
           : _cryptoV2.VerifyData(certificateB64, data, signature);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return false;
         }

         error = string.Empty;
         return true;
      }

      #endregion

      #region VerifyPkcs7

      public bool VerifyPkcs7(byte[] signature, byte[] unsignedData, bool isDataHash, out uint nErrorCode,
        out string error)
      {
         //_cryptoComObject.SetParameterValue("app[DisableCertVerify]", "1");
         nErrorCode = isDataHash
           ? _cryptoV2.pkcs7VerifyHash(signature, unsignedData)
           : _cryptoV2.pkcs7VerifyData(signature, unsignedData);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return false;
         }

         error = string.Empty;
         return true;
      }

      #endregion

      #region GetSignatureAlgorithm

      public HashAlgorithmType? GetSignatureAlgorithm(byte[] signature, out uint nErrorCode, out string error)
      {
         uint algorithm;
         nErrorCode = _cryptoV2.pkcs7GetSignatureAlgorithm(signature, out algorithm);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         HashAlgorithmType? result;
         try
         {
            result = (HashAlgorithmType)algorithm;
         }
         catch
         {
            result = null;
         }

         error = string.Empty;
         return result;
      }

      #endregion

      #region GetCertificateAlgorithm

      public HashAlgorithmType? GetCertificateAlgorithm(string certificateB64, out uint nErrorCode, out string error)
      {
         uint algorithm;
         nErrorCode = _cryptoV2.GetCertificateAlgorithm(certificateB64, out algorithm);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         HashAlgorithmType? result;
         try
         {
            result = (HashAlgorithmType)algorithm;
         }
         catch
         {
            result = null;
         }

         error = string.Empty;
         return result;
      }

      #endregion

      #region CheckCertificate

      public bool CheckCertificate(string certificateB64, string pinCode, StoreLocation? location, out uint nErrorCode, out string error)
      {
         if (location.HasValue)
            _crypto.SetParameterValue("[app]CRYPT_MACHINE_KEYSET",
              location.Value == StoreLocation.CurrentUser ? "0" : "1");
         nErrorCode = _cryptoV2.CheckPrivateKey(certificateB64, pinCode);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return false;
         }
         error = string.Empty;
         return true;
      }

      #endregion

      #endregion

      #region Old interface (GOST 2001 only | no GOST)

      #region GetData

      public byte[] GetData(byte[] signedData, out uint nErrorCode, out string error)
      {
         object unsignedData;
         nErrorCode = _crypto.pkcs7GetDataEx(signedData, out unsignedData);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return (byte[])unsignedData;
      }

      public string GetDataB64(byte[] signedData, out uint nErrorCode, out string error)
      {
         var unsignedData = GetData(signedData, out nErrorCode, out error);
         if (unsignedData == null) return null;
         return Convert.ToBase64String(unsignedData);
      }

      public byte[] GetData(string signedData, out uint nErrorCode, out string error)
      {
         var unsignedData = GetDataB64(signedData, out nErrorCode, out error);
         if (unsignedData == null) return null;
         return Convert.FromBase64String(unsignedData);
      }

      public string GetDataB64(string signedData, out uint nErrorCode, out string error)
      {
         string unsignedData;
         nErrorCode = _crypto.pkcs7GetData(signedData, out unsignedData);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return unsignedData;
      }

      #endregion

      #region GetSignerCertificate

      public byte[] GetSignerCertificate(byte[] signature, out uint nErrorCode, out string error)
      {
         var certificate = GetSignerCertificateB64(signature, out nErrorCode, out error);
         if (certificate == null) return null;
         return Convert.FromBase64String(certificate);
      }

      public byte[] GetSignerCertificate(string signature, out uint nErrorCode, out string error)
      {
         var certificate = GetSignerCertificateB64(signature, out nErrorCode, out error);
         if (certificate == null) return null;
         return Convert.FromBase64String(certificate);
      }

      public string GetSignerCertificateB64(byte[] signature, out uint nErrorCode, out string error)
      {
         string certificate;
         nErrorCode = _crypto.GetSignerCertificateEx(signature, out certificate);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return certificate.Replace("\r\n", "");
      }

      public string GetSignerCertificateB64(string signature, out uint nErrorCode, out string error)
      {
         string certificate;
         nErrorCode = _crypto.GetSignerCertificate(signature, out certificate);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return certificate.Replace("\r\n", "");
      }

      #endregion

      #region GetTypeCode

      public uint GetTypeCode(byte[] data, out uint nErrorCode, out string error)
      {
         uint code;
         nErrorCode = _crypto.pkcs7GetTypeEx(data, out code);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return 0;
         }

         error = string.Empty;
         return code;
      }

      #endregion

      // TODO ГОСТ 2001 используется при проверках, доработать!

      #region GetHash

      public byte[] GetHash(byte[] data, bool stepMode, out uint nErrorCode, out string error)
      {
         var hash = GetHashB64(data, stepMode, out nErrorCode, out error);
         if (hash == null) return null;
         return Convert.FromBase64String(hash);
      }

      public byte[] GetHash(string data, out uint nErrorCode, out string error)
      {
         var hash = GetHashB64(data, out nErrorCode, out error);
         if (hash == null) return null;
         return Convert.FromBase64String(hash);
      }

      public string GetHashB64(byte[] data, bool stepMode, out uint nErrorCode, out string error)
      {
         string hash;

         if (stepMode)
         {
            long handle;
            nErrorCode = _crypto.HashBegin(out handle);
            if (nErrorCode != 0)
            {
               error = _crypto.GetErrorDescription(nErrorCode);
               return null;
            }

            var bodyOffset = 0;
            var length = 1000000;
            var buffer = new byte[length];
            while (bodyOffset < data.Length)
            {
               length = Math.Min(length, data.Length - bodyOffset);
               if (length < buffer.Length)
               {
                  var buffer2 = new byte[length];
                  Buffer.BlockCopy(data, bodyOffset, buffer2, 0, length);
                  nErrorCode = _crypto.HashUpdate(buffer2, handle);
               }
               else
               {
                  Buffer.BlockCopy(data, bodyOffset, buffer, 0, length);
                  nErrorCode = _crypto.HashUpdate(buffer, handle);
               }

               if (nErrorCode != 0)
               {
                  error = _crypto.GetErrorDescription(nErrorCode);
                  _crypto.HashEnd(handle, out hash);
                  return null;
               }

               bodyOffset += length;
            }

            nErrorCode = _crypto.HashEnd(handle, out hash);
            if (nErrorCode != 0)
            {
               error = _crypto.GetErrorDescription(nErrorCode);
               return null;
            }
         }
         else
         {
            nErrorCode = _crypto.HashEx(data, out hash);
            if (nErrorCode != 0)
            {
               error = _crypto.GetErrorDescription(nErrorCode);
               return null;
            }
         }

         error = string.Empty;
         return hash;
      }

      public string GetHashB64(string data, out uint nErrorCode, out string error)
      {
         string hash;
         nErrorCode = _crypto.Hash(data, out hash);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return hash;
      }

      #endregion

      #region GetTimeStampDate

      public DateTime? GetTimeStampDate(byte[] signature, out uint nErrorCode, out string error)
      {
         object date;
         nErrorCode = _crypto.pkcs7GetSigningTimeFromTimestamp(signature, out date);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return (DateTime)date;
      }

      public DateTime? GetTimeStampDate(string signature, out uint nErrorCode, out string error)
      {
         return GetTimeStampDate(Convert.FromBase64String(signature), out nErrorCode, out error);
      }

      #endregion

      #region GetSignedAttributes

      public Attributes GetSignedAttributes(string signature)
      {
         var attributes = _crypto.GetSignedAttributes(signature);
         return attributes;
      }

      public Attributes GetSignedAttributes(byte[] signature)
      {
         var attributes = _crypto.GetSignedAttributesEx(signature);
         return attributes;
      }

      public Tuple<DateTime?, string, Guid?> GetSignedAttributesAll(byte[] signature)
      {
         var attributes = _crypto.GetSignedAttributesEx(signature);

         DateTime? signDate = null;
         var attribute = attributes.Lookup("1.2.840.113549.1.9.5");
         if (!string.IsNullOrEmpty(attribute))
         {
            IFormatProvider formatProvider = new DateTimeFormatInfo();
            signDate = DateTime.ParseExact(attribute, "yyyyMMddHHmmss", formatProvider);
         }

         var description = attributes.Lookup("1.3.6.1.4.1.311.88.2.2");

         Guid? rulDocumentId;
         try
         {
            rulDocumentId = Guid.Parse(
              Encoding.UTF8.GetString(StringConverter.ConvertToByteArray(attributes.Lookup("1.3.6.1.4.1.311.88.2.3.1")))
                .Substring(2));
         }
         catch
         {
            rulDocumentId = null;
         }

         return new Tuple<DateTime?, string, Guid?>(signDate, description, rulDocumentId);
      }

      public DateTime? GetSignedAttributesDate(byte[] signature)
      {
         var attributes = _crypto.GetSignedAttributesEx(signature);
         var time = attributes.Lookup("1.2.840.113549.1.9.5");
         DateTime? signDate = null;
         if (!string.IsNullOrEmpty(time))
         {
            IFormatProvider formatProvider = new DateTimeFormatInfo();
            signDate = DateTime.ParseExact(time, "yyyyMMddHHmmss", formatProvider);
         }

         return signDate;
      }

      public DateTime? GetSignedAttributesDate(string signature)
      {
         var attributes = _crypto.GetSignedAttributes(signature);
         var time = attributes.Lookup("1.2.840.113549.1.9.5");
         DateTime? signDate = null;
         if (!string.IsNullOrEmpty(time))
         {
            IFormatProvider formatProvider = new DateTimeFormatInfo();
            signDate = DateTime.ParseExact(time, "yyyyMMddHHmmss", formatProvider);
         }

         return signDate;
      }

      public string GetSignedAttributesDescription(byte[] signature)
      {
         var attributes = _crypto.GetSignedAttributesEx(signature);
         var description = attributes.Lookup("1.3.6.1.4.1.311.88.2.2");
         return description;
      }

      public string GetSignedAttributesDescription(string signature)
      {
         var attributes = _crypto.GetSignedAttributes(signature);
         var description = attributes.Lookup("1.3.6.1.4.1.311.88.2.2");
         return description;
      }

      public Guid? GetSignedAttributesDocumentId(byte[] signature)
      {
         var attributes = _crypto.GetSignedAttributesEx(signature);
         Guid? documentId;
         try
         {
            documentId = Guid.Parse(
              Encoding.UTF8.GetString(StringConverter.ConvertToByteArray(attributes.Lookup("1.3.6.1.4.1.311.88.2.3.1")))
                .Substring(2));
         }
         catch
         {
            documentId = null;
         }

         return documentId;
      }

      public Guid? GetSignedAttributesDocumentId(string signature)
      {
         var attributes = _crypto.GetSignedAttributes(signature);
         Guid? documentId;
         try
         {
            documentId = Guid.Parse(
              Encoding.UTF8.GetString(StringConverter.ConvertToByteArray(attributes.Lookup("1.3.6.1.4.1.311.88.2.3.1")))
                .Substring(2));
         }
         catch
         {
            documentId = null;
         }

         return documentId;
      }

      #endregion

      #region SignMobile

      public byte[] SignBegin(string hashB64, string certificateB64, uint nTypeFormat, string docName,
        string docDescription,
        out uint nErrorCode, out string error)
      {
         string signHash;
         nErrorCode = _crypto.pkcs7SignBegin(certificateB64, hashB64, nTypeFormat, docName, docDescription,
           out signHash);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return Convert.FromBase64String(signHash);
      }

      public byte[] SignEnd(string certificate, string hash, string signature, uint nTypeFormat, string docName,
        string docDescription, string tspUrl, out uint nErrorCode, out string error)
      {
         string result;
         nErrorCode = _crypto.pkcs7SignEnd(certificate, hash, signature, nTypeFormat, docName, docDescription,
           tspUrl,
           out result);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return Convert.FromBase64String(result);
      }

      #endregion

      #region AddTimeStamp

      public byte[] AddTimeStamp(byte[] signature, string tspUrl, out uint nErrorCode, out string error)
      {
         object result;
         nErrorCode = _crypto.pkcs7AddTimeStampTokenEx(signature, tspUrl, out result);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return (byte[])result;
      }

      public string AddTimeStampB64(byte[] signature, string tspUrl, out uint nErrorCode, out string error)
      {
         var signatureWithTimeStamp = AddTimeStamp(signature, tspUrl, out nErrorCode, out error);
         if (signatureWithTimeStamp == null) return null;
         return Convert.ToBase64String(signatureWithTimeStamp);
      }

      public byte[] AddTimeStamp(string signature, string tspUrl, out uint nErrorCode, out string error)
      {
         var signatureWithTimeStamp = AddTimeStampB64(signature, tspUrl, out nErrorCode, out error);
         if (signatureWithTimeStamp == null) return null;
         return Convert.FromBase64String(signatureWithTimeStamp);
      }

      public string AddTimeStampB64(string signature, string tspUrl, out uint nErrorCode, out string error)
      {
         string result;
         nErrorCode = _crypto.pkcs7AddTimeStampToken(signature, tspUrl, out result);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return result;
      }

      #endregion

      #region CadesBesToCadesXLongType1

      public byte[] CadesBesToCadesXLongType1(byte[] signature, string tspUrl, out uint nErrorCode, out string error)
      {
         object result;
         nErrorCode = _crypto.CadesBESToCadesXLongType1Ex(signature, tspUrl, out result);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return (byte[])result;
      }

      public string CadesBesToCadesXLongType1B64(byte[] signature, string tspUrl, out uint nErrorCode, out string error)
      {
         var result = CadesBesToCadesXLongType1(signature, tspUrl, out nErrorCode, out error);
         if (result == null) return null;
         return Convert.ToBase64String(result);
      }

      public byte[] CadesBesToCadesXLongType1(string signature, string tspUrl, out uint nErrorCode, out string error)
      {
         var result = CadesBesToCadesXLongType1B64(signature, tspUrl, out nErrorCode, out error);
         if (result == null) return null;
         return Convert.FromBase64String(result);
      }

      public string CadesBesToCadesXLongType1B64(string signature, string tspUrl, out uint nErrorCode, out string error)
      {
         string result;
         nErrorCode = _crypto.CadesBESToCadesXLongType1(signature, tspUrl, out result);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return result;
      }

      #endregion

      #region AttachSignature

      public byte[] AttachSignature(byte[] signature, byte[] data, out uint nErrorCode, out string error)
      {
         object result;
         nErrorCode = _crypto.pkcs7AttachDataEx(signature, data, out result);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return (byte[])result;
      }

      public string AttachSignatureB64(byte[] signature, byte[] data, out uint nErrorCode, out string error)
      {
         var result = AttachSignature(signature, data, out nErrorCode, out error);
         if (result == null) return null;
         return Convert.ToBase64String(result);
      }

      public byte[] AttachSignature(string signature, string data, out uint nErrorCode, out string error)
      {
         var result = AttachSignatureB64(signature, data, out nErrorCode, out error);
         if (result == null) return null;
         return Convert.FromBase64String(result);
      }

      public string AttachSignatureB64(string signature, string data, out uint nErrorCode, out string error)
      {
         string result;
         nErrorCode = _crypto.pkcs7AttachData(signature, data, out result);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return result;
      }

      #endregion

      #region DetachSignature

      public Tuple<byte[], byte[], byte[]> DetachSignature(byte[] data, out uint nErrorCode, out string error)
      {
         object unsignedData;
         object signature;
         string hash;
         nErrorCode = _crypto.pkcs7DetachDataEx(data, out unsignedData, out signature, out hash);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return new Tuple<byte[], byte[], byte[]>((byte[])unsignedData, (byte[])signature,
           Convert.FromBase64String(hash));
      }

      public Tuple<string, string, string> DetachSignatureB64(byte[] data, out uint nErrorCode, out string error)
      {
         object unsignedData;
         object signature;
         string hash;
         nErrorCode = _crypto.pkcs7DetachDataEx(data, out unsignedData, out signature, out hash);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return new Tuple<string, string, string>(Convert.ToBase64String((byte[])unsignedData),
           Convert.ToBase64String((byte[])signature), hash);
      }

      public Tuple<byte[], byte[], byte[]> DetachSignature(string data, out uint nErrorCode, out string error)
      {
         string unsignedData;
         string signature;
         string hash;
         nErrorCode = _crypto.pkcs7DetachData(data, out unsignedData, out signature, out hash);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return new Tuple<byte[], byte[], byte[]>(Convert.FromBase64String(unsignedData),
           Convert.FromBase64String(signature), Convert.FromBase64String(hash));
      }

      public Tuple<string, string, string> DetachSignatureB64(string data, out uint nErrorCode, out string error)
      {
         string unsignedData;
         string signature;
         string hash;
         nErrorCode = _crypto.pkcs7DetachData(data, out unsignedData, out signature, out hash);
         if (nErrorCode != 0)
         {
            error = _crypto.GetErrorDescription(nErrorCode);
            return null;
         }

         error = string.Empty;
         return new Tuple<string, string, string>(unsignedData, signature, hash);
      }

      #endregion

      #endregion
   }

   #region Helpers

   internal static class EnumHelper
   {
      /// <summary>
      ///   Gets custom attribute of type T for an enum value.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="value">The value.</param>
      /// <returns></returns>
      internal static T GetAttribute<T>(this Enum value) where T : Attribute
      {
         var type = value.GetType();
         var name = Enum.GetName(type, value);
         return type.GetField(name).GetCustomAttribute<T>();
      }
   }

   internal static class StringConverter
   {
      internal static byte[] ConvertToByteArray(string value)
      {
         byte[] bytes;
         if (string.IsNullOrEmpty(value))
         {
            bytes = new byte[0];
         }
         else
         {
            var stringLength = value.Length;
            var characterIndex = value.StartsWith("0x", StringComparison.Ordinal) ? 2 : 0;
            // Does the string define leading HEX indicator '0x'. Adjust starting index accordingly.               
            var numberOfCharacters = stringLength - characterIndex;

            var addLeadingZero = false;
            if (0 != numberOfCharacters % 2)
            {
               addLeadingZero = true;

               numberOfCharacters += 1; // Leading '0' has been striped from the string presentation.
            }

            bytes = new byte[numberOfCharacters / 2]; // Initialize our byte array to hold the converted string.

            var writeIndex = 0;
            if (addLeadingZero)
            {
               bytes[writeIndex++] = FromCharacterToByte(value[characterIndex], characterIndex);
               characterIndex += 1;
            }

            for (var readIndex = characterIndex; readIndex < value.Length; readIndex += 2)
            {
               var upper = FromCharacterToByte(value[readIndex], readIndex, 4);
               var lower = FromCharacterToByte(value[readIndex + 1], readIndex + 1);

               bytes[writeIndex++] = (byte)(upper | lower);
            }
         }

         return bytes;
      }

      private static byte FromCharacterToByte(char character, int index, int shift = 0)
      {
         var value = (byte)character;
         if (0x40 < value && 0x47 > value || 0x60 < value && 0x67 > value)
         {
            if (0x40 == (0x40 & value))
               if (0x20 == (0x20 & value))
                  value = (byte)((value + 0xA - 0x61) << shift);
               else
                  value = (byte)((value + 0xA - 0x41) << shift);
         }
         else if (0x29 < value && 0x40 > value)
         {
            value = (byte)((value - 0x30) << shift);
         }
         else
         {
            throw new InvalidOperationException(
              $"Character '{character}' at index '{index}' is not valid alphanumeric character.");
         }

         return value;
      }
   }

   #endregion
}
