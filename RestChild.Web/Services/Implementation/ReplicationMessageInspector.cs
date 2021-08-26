using System.Collections.ObjectModel;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;

namespace RestChild.Web.Services.Implementation
{
	/*
     Этот инспектор нужен для исправления сообщения от АС УР. В их сообщении у входного параметра 
     метода отсутствует namespace nsi, метод AfterReceiveRequest добавляет необходимый префикс.
     */

	public class ReplicationMessageInspector : System.Attribute, IServiceBehavior, IDispatchMessageInspector
	{
		//private LoggingAsUrNsi logger = new LoggingAsUrNsi();

		public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			string body;
			MemoryStream memoryStream;
			using (memoryStream = new MemoryStream())
			{
				using (XmlWriter xw = XmlWriter.Create(memoryStream))
				{
					request.WriteMessage(xw);
					xw.Flush();
					body = Encoding.UTF8.GetString(memoryStream.ToArray());
					xw.Close();
				}
				body = body.Replace("<xml>", "<nsi:xml>").Replace("</xml>", "</nsi:xml>");
			}
			memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(body));
			//logger.SaveMessage(true, body, "receive_change");
			XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(memoryStream, XmlDictionaryReaderQuotas.Max); // new XmlDictionaryReaderQuotas()); // 
			Message newMessage = Message.CreateMessage(reader, int.MaxValue, request.Version);
			newMessage.Properties.CopyProperties(request.Properties);
			request = newMessage;

			return null;
		}

		public void BeforeSendReply(ref Message reply, object correlationState)
		{
			//logger.SaveMessage(false, reply.ToString(), "receive_change");
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
					epDisp.DispatchRuntime.MessageInspectors.Add(new ReplicationMessageInspector());
				}
			}
		}
	}
}