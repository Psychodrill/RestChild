using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using UtilsSmev.Interface;

namespace UtilsSmev.Inspectors
{
	public class SmevClientBehavior : IEndpointBehavior
	{
		ILoggingSmevRequest Logging { get; set; }
		private List<string> _xpaths;
		private List<string> _ids;
		private List<string> _namespases;
		private List<string> _prefix;
		private string _actor;

		public SmevClientBehavior()
		{
		}

		public SmevClientBehavior(ILoggingSmevRequest logging, List<string> xpaths, List<string> ids, List<string> namespases, List<string> prefix, string actor)
		{
			Logging = logging;
			_xpaths = xpaths;
			_ids = ids;
			_namespases = namespases;
			_prefix = prefix;
			_actor = actor;
		}

		public void Validate(ServiceEndpoint endpoint)
		{
			// Пусто
		}

		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
			// Пусто
		}

		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			// Пусто
		}

		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			clientRuntime.MessageInspectors.Add(new SmevClientInspector(Logging, _xpaths, _ids, _namespases, _prefix, _actor));
		}
	}
}
