using System.Configuration;

namespace MailingDemon.Tasks
{
	[Task]
	public class MqListenerStatus : MqListener
	{
		protected override void Execute()
		{
			GetMessagesFromUts(ConfigurationManager.AppSettings["MqRequestStatusIncoming"]);
		}
	}
}