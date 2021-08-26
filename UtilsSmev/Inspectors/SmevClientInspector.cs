using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;
using UtilsSmev.Interface;
using log4net;

namespace UtilsSmev.Inspectors
{
	public class SmevClientInspector : Attribute,
	                                   IServiceBehavior,
	                                   IClientMessageInspector
	{
		private readonly object _clientCertThumbprint = ConfigurationManager.AppSettings["clientCert"];

		private readonly X509FindType _findType =
			(X509FindType) Enum.Parse(typeof (X509FindType), ConfigurationManager.AppSettings["clientCertFindType"], true);

		private readonly List<string> _ids;
		private readonly List<string> _namespases;
		private readonly string _pinCode = ConfigurationManager.AppSettings["clientCertPinCode"] ?? string.Empty;
		private readonly List<string> _prefix;

		private readonly X509Certificate2 _serviceCert;

		private readonly StoreLocation _storeLocation =
			(StoreLocation) Enum.Parse(typeof (StoreLocation), ConfigurationManager.AppSettings["clientCertStoreLocation"], true);

		private readonly StoreName _storeName =
			(StoreName) Enum.Parse(typeof (StoreName), ConfigurationManager.AppSettings["clientCertStoreName"], true);

		private readonly List<string> _xpaths;

		private readonly string _actor ;

		public SmevClientInspector(ILoggingSmevRequest logging, List<string> xpaths, List<string> ids, List<string> namespases,
															 List<string> prefix, string actor)
		{
			_xpaths = xpaths;
			_ids = ids;
			_namespases = namespases;
			_prefix = prefix;
			Logging = logging;
			var store = new X509Store(_storeName, _storeLocation);
			store.Open(OpenFlags.ReadOnly);
			_actor = actor;
			//сертификат
			X509Certificate2Collection coll = store.Certificates.Find(_findType, _clientCertThumbprint, false);

			if (coll.Count == 0)
			{
				var sb = new StringBuilder();
				for (int i = 0; i < store.Certificates.Count; i++)
				{
					string a = store.Certificates[i].Thumbprint.ToUpper();
					string b = _clientCertThumbprint.ToString().Trim().Replace(" ", string.Empty).ToUpper();


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
					                       _clientCertThumbprint, sb);
					throw new FileNotFoundException(string.Format("Сертификат сервера не найден. Отпечаток {0}; Сертификаты в хранилище: {1}",
										   _clientCertThumbprint, sb));
				}
			}
			else
			{
				_serviceCert = coll[0];
			}
		}

		public SmevClientInspector(string certificate)
		{
			var store = new X509Store(_storeName, _storeLocation);
			store.Open(OpenFlags.ReadOnly);

			//сертификат
			X509Certificate2Collection coll = store.Certificates.Find(_findType, certificate, true);

			if (coll.Count == 0)
			{
				throw new FileNotFoundException(string.Format("Сертификат клиента не найден. Отпечаток {0}", certificate));
			}
			_serviceCert = coll[0];
		}

		public SmevClientInspector(X509Certificate2 certificate, string pinCode)
		{
			_serviceCert = certificate;
			_pinCode = pinCode;
		}

		private ILoggingSmevRequest Logging { get; set; }
		private string _action;

      public virtual object BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			_action = null;
			string outgoingMessage = request.ToString();

			var doc = new XmlDocument();
			doc.LoadXml(outgoingMessage);

			string signedSoapMessage = _xpaths != null && _xpaths.Count > 0
				                           ? Signer.SignMessageTopCase(outgoingMessage, "ep-ov", _serviceCert, _storeLocation, _pinCode, _xpaths,
																															 _ids, _namespases, _prefix, _actor)
																	 : Signer.SignMessageTopCase(outgoingMessage, "ep-ov", _serviceCert, _storeLocation, _pinCode, _actor);
			if (Logging != null)
			{
				_action = request.Headers.Action;
				Logging.SaveMessage(false, signedSoapMessage, request.Headers.Action);
			}
//			LogManager.GetLogger("Xml").Info(signedSoapMessage);

			var ms = new MemoryStream(Encoding.UTF8.GetBytes(signedSoapMessage));
			XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(ms, new XmlDictionaryReaderQuotas());
			request = Message.CreateMessage(reader, Int32.MaxValue, request.Version);

			return null;
		}

		public virtual void AfterReceiveReply(ref Message reply, object correlationState)
		{
         Logging?.SaveMessage(true, reply.ToString(), reply.Headers.Action ?? _action);
//			LogManager.GetLogger("Xml").Info(reply.ToString());

			var req = reply.ToString().Replace(@"mustUnderstand=""1""", @"mustUnderstand=""0""");
			var ms = new MemoryStream(Encoding.UTF8.GetBytes(req));
			XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(ms, new XmlDictionaryReaderQuotas());
			reply = Message.CreateMessage(reader, Int32.MaxValue, reply.Version);

		}

		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			// Пусто
		}

		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
		                                 Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
			// Пусто
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			// Пусто
		}

		public void ApplyClientBehavior(
			ServiceEndpoint endpoint,
			ClientRuntime clientRuntime)
		{
			clientRuntime.MessageInspectors.Add(this);
		}
	}
}
