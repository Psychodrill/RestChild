using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using MailingDemon.Tasks;

namespace MailingDemon.Common
{
	public class ConfigManager
	{
		private Hashtable configuredPresets;
		private BaseConfigStorage defaultPreset;
		private XNameValueCollection appSettings;
		public static ConfigManager AppSettings
		{
			get
			{
                // ReSharper disable once ArrangeAccessorOwnerBody
                return (ConfigManager)ConfigurationSettings.GetConfig("settings");
			}
		}
		public string this[string name]
		{
			get
			{
				return this.appSettings[name];
			}
		}
		public ConfigManager()
		{
			this.configuredPresets = new Hashtable();
		}
		public BaseConfig Configure(Type type)
		{
			return this.defaultPreset.Restore(type, new Type[0]);
		}
		public BaseConfig Configure(Type type, ArrayList extraTypes)
		{
			Type[] extraTypes2 = (Type[])extraTypes.ToArray(typeof(Type));
			return this.defaultPreset.Restore(type, extraTypes2);
		}
		public bool KeyExists(string configKey)
		{
			return this.configuredPresets.ContainsKey(configKey);
		}
		public void AddPreset(BaseConfigStorage preset, string key)
		{
			this.configuredPresets.Add(key, preset);
		}
		public void SetDefaultPreset(string defaultPresetList)
		{
			string[] array = defaultPresetList.Split(new char[]
			{
				',',
				' '
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				BaseConfigStorage baseConfigStorage = (BaseConfigStorage)this.configuredPresets[text];
				if (baseConfigStorage != null)
				{
					this.defaultPreset = baseConfigStorage;
					return;
				}
			}
			throw new ConfigurationErrorsException("Default preset was not found in the configured preset list.");
		}
		public void LoadNameValueCollections(NameValueCollection baseCollection)
		{
			BaseConfig[] array = this.defaultPreset.RestoreAll(typeof(XNameValueCollection), string.Empty, new Type[0]);
			this.appSettings = new XNameValueCollection(baseCollection);
			for (int i = array.Length - 1; i >= 0; i--)
			{
				this.appSettings.Apply((XNameValueCollection)array[i]);
			}
		}
		public BaseConfigStorage ResolveReference(string key)
		{
			return (BaseConfigStorage)this.configuredPresets[key];
		}
	}
}
