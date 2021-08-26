using System;
using System.Collections;
using System.Configuration;
using log4net;
using MailingDemon.Tasks;

namespace MailingDemon.Common
{
	public abstract class BaseConfigStorage
	{
		private readonly ConfigManager _baseReferenceResolver;
		protected string BaseStorageName;
		protected ILog Logger;

		public BaseConfigStorage(ConfigManager referenceResolver)
		{
			Logger = LogManager.GetLogger(base.GetType());
			_baseReferenceResolver = referenceResolver;
		}

		protected BaseConfigStorage baseStorage
		{
			get
			{
				if (BaseStorageName == null)
				{
					return null;
				}
				return _baseReferenceResolver.ResolveReference(BaseStorageName);
			}
		}

		public abstract void Store(BaseConfig configuration);

		public BaseConfig Restore(Type configType, params Type[] extraTypes)
		{
			return Restore(configType, string.Empty, extraTypes);
		}

		protected abstract BaseConfig restore(Type configType, string name, params Type[] extraTypes);
		public abstract bool Contains(Type configType, string name);

		public BaseConfig Restore(Type configType, string name, params Type[] extraTypes)
		{
			BaseConfig baseConfig = RestoreFromThisOrParent(configType, name, extraTypes);
			if (baseConfig == null)
			{
				throw new ConfigurationErrorsException(string.Format("Config object of type {0} with name {1} was not found.", configType,
					name));
			}
			baseConfig.Validate(baseConfig);
			baseConfig.OnRestore();
			return baseConfig;
		}

		private BaseConfig RestoreFromThisOrParent(Type configType, string name, Type[] extraTypes)
		{
			BaseConfig result = null;
			if (Contains(configType, name))
			{
				result = restore(configType, name, extraTypes);
			}
			else
			{
				if (baseStorage != null)
				{
					result = baseStorage.RestoreFromThisOrParent(configType, name, extraTypes);
				}
			}
			return result;
		}

		public BaseConfig[] RestoreAll(Type configType, string name, params Type[] extraTypes)
		{
			var arrayList = new ArrayList();
			RestoreFromThisAndParent(configType, name, extraTypes, arrayList);
			var array = new BaseConfig[arrayList.Count];
			int num = 0;
			foreach (BaseConfig baseConfig in arrayList)
			{
				baseConfig.Validate(baseConfig);
				baseConfig.OnRestore();
				array[num++] = baseConfig;
			}

			return array;
		}

		private void RestoreFromThisAndParent(Type configType, string name, Type[] extraTypes, ArrayList container)
		{
			if (Contains(configType, name))
			{
				container.Add(restore(configType, name, extraTypes));
			}
			if (baseStorage != null)
			{
				baseStorage.RestoreFromThisAndParent(configType, name, extraTypes, container);
			}
		}
	}
}