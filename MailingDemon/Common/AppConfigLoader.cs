using log4net;
using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Xml;

namespace MailingDemon.Common
{
	public class AppConfigLoader : IConfigurationSectionHandler
	{
		private static ILog logger = LogManager.GetLogger("Configuration loader");
		private AppConfigLoader()
		{
		}
		public object Create(object parent, object configContext, XmlNode section)
		{
			ConfigManager configManager = new ConfigManager();
			XmlNodeList xmlNodeList = section.SelectNodes("preset");
			foreach (XmlNode node in xmlNodeList)
			{
				this.LoadPreset(configManager, node);
			}

			if (section.Attributes["default"] == null)
			{
				throw new ConfigurationErrorsException("No default preset defined", section);
			}
			configManager.SetDefaultPreset(section.Attributes["default"].Value);
			configManager.LoadNameValueCollections(ConfigurationManager.AppSettings);
			return configManager;
		}
		protected BaseConfigStorage LoadPreset(ConfigManager configMan, XmlNode node)
		{
			string text = string.Empty;
			BaseConfigStorage baseConfigStorage = null;
			if (node.Attributes["key"] != null)
			{
				text = node.Attributes["key"].Value;
				AppConfigLoader.logger.InfoFormat("Loading '{0}' configuration preset.", text);
				XmlAttribute xmlAttribute = node.Attributes["storage"];
				if (xmlAttribute != null)
				{
					string text2;
					if ((text2 = xmlAttribute.Value) != null)
					{
						text2 = string.IsInterned(text2);
						if (text2 == "file")
						{
							XmlAttribute xmlAttribute2 = node.Attributes["file"];
							if (xmlAttribute2 == null)
							{
								throw new ConfigurationErrorsException("No filename defined", node);
							}
							string text3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlAttribute2.Value);
							if (File.Exists(text3))
							{
								baseConfigStorage = new ExternalFileStorage(configMan, text3);
								goto IL_112;
							}
							if (node.Attributes["optional"] == null)
							{
								throw new ConfigurationErrorsException(string.Format("External file not found at {0}", text3), node);
							}
							goto IL_112;
						}
					}
					throw new ConfigurationErrorsException("Unknown storage method", node);
				}
				baseConfigStorage = new AppConfigStorage(configMan, node);
			IL_112:
				if (baseConfigStorage != null)
				{
					configMan.AddPreset(baseConfigStorage, text);
				}
				return baseConfigStorage;
			}
			throw new ConfigurationErrorsException("No preset key defined", node);
		}
	}
}
