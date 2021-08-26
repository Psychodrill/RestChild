using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace RestChild.Booking.Logic
{
	/*
     Этот инспектор нужен для исправления сообщения от АС УР. В их сообщении у входного параметра 
     метода отсутствует namespace nsi, метод AfterReceiveRequest добавляет необходимый префикс.
     */

	public class LoggingInspector : System.Attribute, IServiceBehavior, IDispatchMessageInspector
	{
		
		public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			log4net.LogManager.GetLogger("RequestResponse").Info(string.Format("AfterReceiveRequest:\n{0}", request));
			return null;
		}

		public void BeforeSendReply(ref Message reply, object correlationState)
		{
			log4net.LogManager.GetLogger("RequestResponse").Info(string.Format("BeforeSendReply:\n{0}", reply));
		}

		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
		                                 Collection<ServiceEndpoint> endpoints,
		                                 BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcher chDisp in serviceHostBase.ChannelDispatchers)
			{
				foreach (EndpointDispatcher epDisp in chDisp.Endpoints)
				{
					epDisp.DispatchRuntime.MessageInspectors.Add(new LoggingInspector());
				}
			}
		}
	}
}