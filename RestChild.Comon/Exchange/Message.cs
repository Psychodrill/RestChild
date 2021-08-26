using System;
using System.Xml.Serialization;
using RestChild.Common.Service.ServiceReference;

namespace RestChild.Comon.Exchange
{
    [Serializable]
    [XmlType(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v5/")]
    [XmlRoot("Message", Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v5/")]
    public class Message : CoordinateData
    {
    }
}
