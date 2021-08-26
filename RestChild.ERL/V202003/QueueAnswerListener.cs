using System;
using System.IO;
using System.Xml.Serialization;
using Apache.NMS;
using log4net;
using RestChild.Comon;
using RestChild.DAL;
using Spring.Messaging.Nms.Core;

namespace RestChild.ERL.V202003
{
    /// <summary>
    ///     класс для приема сообщения
    /// </summary>
    public class QueueAnswerListener : IMessageListener
    {
        private const string sucsess_result_code = "3";

        private ILog _log;

        public QueueAnswerListener(ILog logger)
        {
            _log = logger;
        }

        public void OnMessage(IMessage message)
        {
            var textMessage = message as ITextMessage;
            var serializer = new XmlSerializer(typeof(Envelope));
            try
            {
                Envelope envelope = null;
                message_response result = null;

                _log.Info(textMessage.Text);

                using (TextReader reader = new StringReader(textMessage.Text))
                {
                    envelope = (Envelope)serializer.Deserialize(reader);
                    result = (message_response)envelope.Body.Item;
                }

                var messageId = Guid.Parse(result.response.request_identity.message_id);

                var sucsess = string.Equals(result.response.result.code, sucsess_result_code, StringComparison.OrdinalIgnoreCase);

                _log.Info($"Message {messageId} recived by ERL (State: {result.response.result.code})");

                if(sucsess)
                {
                    using (var uw = new UnitOfWork())
                    {
                        ErlRepository.ERLMessageIsCommited(uw, messageId);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
    }
}
