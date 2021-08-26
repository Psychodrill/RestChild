using System;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;

namespace UtilsSmev
{
	public class WsdlTuner
	{
		public static bool IsWsdl(string message)
		{
			return !String.IsNullOrWhiteSpace(message) && message.Contains("<wsdl:definitions") && message.EndsWith("</wsdl:definitions>");
		}

		public static Message CustomWsdl(MessageVersion messageVersion, string path)
		{
			XmlReader reader = XmlReader.Create(path, new XmlReaderSettings() {IgnoreWhitespace = false, IgnoreComments = false, IgnoreProcessingInstructions = false, });
			
			/*var doc = new XmlDocument();			
			doc.Load(path);
			var m = Message.CreateMessage(messageVersion, string.Empty, doc);
			m.ToString();*/
			return Message.CreateMessage(reader, int.MaxValue, messageVersion);
		}
	}
}
