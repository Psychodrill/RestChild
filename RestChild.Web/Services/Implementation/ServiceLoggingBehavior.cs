using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;
using UtilsSmev;
using UtilsSmev.Interface;

namespace RestChild.Web.Services.Implementation
{
	public class ServiceLoggingBehavior : BehaviorExtensionElement, IServiceBehavior, IDispatchMessageInspector
	{
		private const string LoggingInstanceTypeName = "loggingInstanceType";
		private const string CustomWsdlPathName = "CustomWsdlPath";

		private ILoggingSmevRequest _logging;

		[ConfigurationProperty(LoggingInstanceTypeName, IsRequired = false)]
		public string LoggingInstanceType
		{
			get { return (string) base[LoggingInstanceTypeName]; }

			set { base[LoggingInstanceTypeName] = value; }
		}

		[ConfigurationProperty(CustomWsdlPathName, IsRequired = false)]
		public string CustomWsdlPath
		{
			get { return (string) base[CustomWsdlPathName]; }

			set { base[CustomWsdlPathName] = value; }
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

		public override Type BehaviorType => typeof (ServiceLoggingBehavior);

		public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			try
			{
				if (request == null)
					return null;

				var s = request.ToString();

				if (string.IsNullOrEmpty(s))
				{
					return null;
				}

				var xml = new XmlDocument();
				xml.LoadXml(s);

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

			return null;
		}

		public void BeforeSendReply(ref Message reply, object correlationState)
		{
			try
			{
				var s = reply.ToString();

				if (string.IsNullOrEmpty(s) || s.StartsWith("<HTML>"))
				{
					return;
				}

				var xml = new XmlDocument();

				xml.LoadXml(s);

				if (!xml.DocumentElement.Name.EndsWith("Envelope"))
				{
					if (!string.IsNullOrWhiteSpace(CustomWsdlPath) && WsdlTuner.IsWsdl(reply.ToString()))
						reply = WsdlTuner.CustomWsdl(reply.Version, CustomWsdlPath);
					return;
				}
			}
			catch
			{
				return;
			}

			Logging?.SaveMessage(false, reply.ToString(), OperationContext.Current.IncomingMessageHeaders.Action);
		}

		void IServiceBehavior.Validate(ServiceDescription service, ServiceHostBase host)
		{
		}

		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
			Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcher cDispatcher in serviceHostBase.ChannelDispatchers)
				foreach (var eDispatcher in cDispatcher.Endpoints)
					eDispatcher.DispatchRuntime.MessageInspectors.Add(this);
		}

		private static object GetBusinessEntityType(string typeName)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

			foreach (var assembly in assemblies)
			{
				var t = assembly.GetType(typeName, false);
				if (t != null)
					return Activator.CreateInstance(t);
			}

			throw new ArgumentException("Type " + typeName + " doesn't exist in the current app domain");
		}

		protected override object CreateBehavior()
		{
			return new ServiceLoggingBehavior
			{
				LoggingInstanceType = LoggingInstanceType,
				CustomWsdlPath = CustomWsdlPath
			};
		}
	}
}
