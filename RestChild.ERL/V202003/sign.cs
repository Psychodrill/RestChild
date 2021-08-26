using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RestChild.ERL.V202003
{
    [Serializable]
    public class Body
    {
        [XmlElement(ElementName = "message_response", Type = typeof(message_response), Namespace = "http://erl.msr.com/schemas/oiv/mq")]
        [XmlElement(ElementName = "oiv_persons_outgoing", Type = typeof(oiv_persons_outgoing), Namespace = "http://erl.msr.com/schemas/oiv/mq")]
        [XmlElement(ElementName = "citizen_benefits_incoming", Type = typeof(citizen_benefits_incoming), Namespace = "http://erl.msr.com/schemas/oiv/mq")]
        [XmlElement(ElementName = "oiv_persons_incoming", Type = typeof(oiv_persons_incoming), Namespace = "http://erl.msr.com/schemas/oiv/mq")]
        public object Item { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "Envelope", Namespace = "http://erl.msr.com/schemas/sign-package")]
    public class Envelope
    {
        [XmlElement(ElementName = "Body")]
        public Body Body { get; set; }

        [XmlAttribute(AttributeName = "sign")]
        public string Sign { get; set; }
    }
}
