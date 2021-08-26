using System;
using System.IO;
using System.Xml;

namespace MailingDemon.Common
{
	public class ExternalFileStorage : XmlNodeConfigStorage
	{
		public ExternalFileStorage(ConfigManager referenceResolver, string fileName)
			: base(referenceResolver)
		{
			if (File.Exists(fileName))
			{
				var xmlDocument = new XmlDocument();
				xmlDocument.Load(fileName);
				LoadXmlNode(xmlDocument.DocumentElement);
				return;
			}
			throw new Exception(string.Format("External config  file {0} was not found.", fileName));
		}
	}
}