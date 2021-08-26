using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RestChild.Comon
{
    public static class Serialization
    {
        public static T Deserialize<T>(string data, XmlSerializer serializer)
        {
            using (var reader = XmlReader.Create(new StringReader(data)))
            {
                return (T) serializer.Deserialize(reader);
            }
        }

        public static T Deserialize<T>(string data)
        {
            var ser = new XmlSerializer(typeof(T));

            using (var reader = XmlReader.Create(new StringReader(data)))
            {
                return (T) ser.Deserialize(reader);
            }
        }

        public static string Serializer<T>(T obj)
        {
            var ser = new XmlSerializer(typeof(T));
            var sb = new StringBuilder();
            using (var tw = new StringWriter(sb, new CultureInfo("RU-ru")))
            {
                ser.Serialize(tw, obj);
            }

            return sb.ToString();
        }

        public static string SerializerDataContract<T>(T obj)
        {
            var ser =
                new DataContractSerializer(typeof(T));
            var sb = new StringBuilder();
            using (var writer = XmlWriter.Create(sb))
            {
                ser.WriteObject(writer, obj);
            }

            return sb.ToString();
        }

        public static T DeserializerDataContract<T>(string data)
        {
            var ser =
                new DataContractSerializer(typeof(T));
            using (var reader = XmlReader.Create(new StringReader(data)))
            {
                return (T) ser.ReadObject(reader);
            }
        }
    }
}
