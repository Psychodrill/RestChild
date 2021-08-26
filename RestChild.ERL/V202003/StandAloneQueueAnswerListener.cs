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
    public class StandAloneQueueAnswerListener : IMessageListener
    {
        private readonly ILog _log;

        public StandAloneQueueAnswerListener(ILog logger)
        {
            _log = logger;
        }

        /// <summary>
        ///     получили сообщение
        /// </summary>
        public void OnMessage(IMessage message)
        {
            var textMessage = message as ITextMessage;
            var serializer = new XmlSerializer(typeof(Envelope));

            try
            {
                Envelope envelope = null;
                oiv_persons_outgoing oiv_Persons_Outgoing = null;

                _log.Info(textMessage?.Text);

                using (TextReader reader = new StringReader(textMessage?.Text))
                {
                    envelope = (Envelope)serializer.Deserialize(reader);
                    oiv_Persons_Outgoing = (oiv_persons_outgoing)envelope.Body.Item;
                }

                var persons = oiv_Persons_Outgoing?.persons ?? new person_outgoing[0];

                var messageId = Guid.Parse(oiv_Persons_Outgoing.identity.message_id);

                _log.Info($"PersonOutgoing.Length={persons.Length}");

                using (var uw = new UnitOfWork())
                {
                    foreach (var person in persons)
                    {
                        var localPersonId = person.provider_identifier;
                        var pid = Guid.Parse(person.citizen_pk);
                        if (!string.IsNullOrWhiteSpace(localPersonId))
                        {
                            ErlRepository.ChildCitizenPkSet(uw, localPersonId, pid, messageId);
                        }
                    }

                    uw.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
    }
}
