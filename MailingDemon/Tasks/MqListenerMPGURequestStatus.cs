using System.Configuration;
using System.Threading.Tasks;

namespace MailingDemon.Tasks
{
   [Task]
   public class MqListenerMPGURequestStatus : MqListener
   {
      protected override void Execute()
      {
         GetMessagesFromUts(ConfigurationManager.AppSettings["MqMPGURequestStatusIncoming"]);
      }
   }
}
