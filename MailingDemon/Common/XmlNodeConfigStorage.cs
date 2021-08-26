using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using MailingDemon.Tasks;

namespace MailingDemon.Common
{
	public class XmlNodeConfigStorage : BaseConfigStorage
	{
		protected Hashtable Cache;
		protected Hashtable Content;

		public XmlNodeConfigStorage(ConfigManager referenceResolver)
			: base(referenceResolver)
		{
		}

		protected void LoadXmlNode(XmlNode data)
		{
			Content = new Hashtable(data.ChildNodes.Count);
			Cache = new Hashtable(data.ChildNodes.Count);
			foreach (XmlNode xmlNode in data.ChildNodes)
			{
				if (xmlNode.NodeType == (XmlNodeType) 1)
				{
					string objectName = string.Empty;
					XmlAttribute xmlAttribute = xmlNode.Attributes["ObjectName"];
					if (xmlAttribute != null)
					{
						objectName = xmlAttribute.Value;
					}
					string key = GetKey(xmlNode.LocalName, objectName);
					Content[key] = new XmlNodeReader(xmlNode);
				}
			}

			XmlAttribute xmlAttribute2 = data.Attributes["base"];
			if (xmlAttribute2 != null)
			{
				string value = xmlAttribute2.Value;
				Logger.InfoFormat("Base preset for this one is: {0}", value);
				BaseStorageName = value;
			}
		}

		protected override BaseConfig restore(Type configType, string name, params Type[] extraTypes)
		{
			string key = GetKey(configType, name);
			var baseConfig = (BaseConfig) Cache[key];
			if (baseConfig == null)
			{
				var xmlNodeReader = (XmlNodeReader) Content[key];
				var xmlSerializer = new XmlSerializer(configType, extraTypes);
				baseConfig = (BaseConfig) xmlSerializer.Deserialize(xmlNodeReader);
				Cache[key] = baseConfig;
			}
			return baseConfig;
		}

		public override bool Contains(Type configType, string name)
		{
			string key = GetKey(configType, name);
			return Content.ContainsKey(key);
		}

		public override void Store(BaseConfig configuration)
		{
			throw new NotImplementedException("No saving is possible now for xml nodes");
		}

		protected string GetKey(Type type, string objectName)
		{
			var xmlTypeAttribute = (XmlTypeAttribute) Attribute.GetCustomAttribute(type, typeof (XmlTypeAttribute));
			string typeName = (xmlTypeAttribute == null) ? type.Name : xmlTypeAttribute.TypeName;
			return GetKey(typeName, objectName);
		}

		private string GetKey(string typeName, string objectName)
		{
			return string.Format("{0}/{1}", typeName, objectName);
		}
	}
}