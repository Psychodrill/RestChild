using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Configuration;
using UtilsSmev.Interface;

namespace UtilsSmev.Inspectors
{
	public class SmevBehaviorExtensionElement : BehaviorExtensionElement
	{
		public override Type BehaviorType
		{
			get { return typeof (SmevClientBehavior); }
		}

		private const string ActorName = "Actor";
		public List<string> Xpaths { get { return string.IsNullOrEmpty(XpathsStrings) ? new List<string>() : XpathsStrings.Split('|').Where(s => !string.IsNullOrEmpty(s)).ToList(); } }
		public List<string> Ids { get { return string.IsNullOrEmpty(IdsStrings) ? new List<string>() : IdsStrings.Split('|').Where(s => !string.IsNullOrEmpty(s)).ToList(); } }
		public List<string> Namespases { get { return string.IsNullOrEmpty(NamespasesStrings) ? new List<string>() : NamespasesStrings.Split('|').Where(s => !string.IsNullOrEmpty(s)).ToList(); } }
		public List<string> Prefix { get { return string.IsNullOrEmpty(PrefixStrings) ? new List<string>() : PrefixStrings.Split('|').Where(s=>!string.IsNullOrEmpty(s)).ToList(); } }

		[ConfigurationProperty(ActorName, IsRequired = false, DefaultValue = "RSMEVAUTH")]
		public string Actor
		{
			get { return String.IsNullOrWhiteSpace((string)base[ActorName]) ? "RSMEVAUTH" : (string)base[ActorName]; }

			set { base[ActorName] = value; }
		}
		protected override object CreateBehavior()
		{
			ILoggingSmevRequest logging = null;
			if (!string.IsNullOrEmpty(LoggingInstanceType))
			{
				logging = GetBusinessEntityType(LoggingInstanceType) as ILoggingSmevRequest;
			}

			return new SmevClientBehavior(logging, Xpaths, Ids, Namespases, Prefix, Actor);
		}

		private const string LoggingInstanceTypeName = "loggingInstanceType";

		[ConfigurationProperty(LoggingInstanceTypeName, IsRequired = false)]
		public string LoggingInstanceType
		{
			get { return (string)base[LoggingInstanceTypeName]; }

			set { base[LoggingInstanceTypeName] = value; }
		}

		private const string XpathsStringsName = "XpathsStrings";

		[ConfigurationProperty(XpathsStringsName, IsRequired = false)]
		public string XpathsStrings
		{
			get { return (string)base[XpathsStringsName]; }

			set { base[XpathsStringsName] = value; }
		}

		private const string IdsStringsName = "IdsStrings";

		[ConfigurationProperty(IdsStringsName, IsRequired = false)]
		public string IdsStrings
		{
			get { return (string)base[IdsStringsName]; }

			set { base[IdsStringsName] = value; }
		}

		private const string NamespasesStringsName = "NamespasesStrings";

		[ConfigurationProperty(NamespasesStringsName, IsRequired = false)]
		public string NamespasesStrings
		{
			get { return (string)base[NamespasesStringsName]; }

			set { base[NamespasesStringsName] = value; }
		}

		private const string PrefixStringsName = "PrefixStrings";

		[ConfigurationProperty(PrefixStringsName, IsRequired = false)]
		public string PrefixStrings
		{
			get { return (string)base[PrefixStringsName]; }

			set { base[PrefixStringsName] = value; }
		}

		private static object GetBusinessEntityType(string typeName)
		{
			List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

			foreach (var assembly in assemblies)
			{
				Type t = assembly.GetType(typeName, false);
				if (t != null)
					return Activator.CreateInstance(t); ;
			}

			throw new ArgumentException("Type " + typeName + " doesn't exist in the current app domain");
		}
	}
}