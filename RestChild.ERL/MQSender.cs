using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.Util;
using Spring.Messaging.Nms.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.ERL
{
   public static class MQSender
   {
      public static void Send(string QueueName, string message)
      {
         string _factory = System.Configuration.ConfigurationManager.AppSettings["ERLConnectionFactory"] ?? "tcp://37.230.240.41:61616";
         string _userName = System.Configuration.ConfigurationManager.AppSettings["ERLConnectionUserName"] ?? "aisdouser";
         string _password= System.Configuration.ConfigurationManager.AppSettings["ERLConnectionPassword"] ?? "aisdopass";


         ConnectionFactory connectionFactory = new ConnectionFactory(_factory);
         connectionFactory.UserName = _userName;
         connectionFactory.Password = _password;
         NmsTemplate template = new NmsTemplate(connectionFactory);
         template.ConvertAndSend(QueueName, message);
      }
   }
}
