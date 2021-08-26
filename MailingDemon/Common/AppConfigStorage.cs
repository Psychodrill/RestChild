using System.Xml;

namespace MailingDemon.Common
{
	public class AppConfigStorage : XmlNodeConfigStorage
	{
		public AppConfigStorage(ConfigManager referenceResolver, XmlNode data)
			: base(referenceResolver)
		{
			LoadXmlNode(data);
		}
	}
}