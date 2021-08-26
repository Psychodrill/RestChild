using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using UtilsSmev.Interface;

namespace UtilsSmev.SmevServiceEncoder
{
	public class SmevServiceMessageEncodingBindingElementExtensionElement : BindingElementExtensionElement
	{
		private const string LoggingInstanceTypeName = "loggingInstanceType";

		[ConfigurationProperty(LoggingInstanceTypeName, IsRequired = false)]
		public string LoggingInstanceType
		{
			get { return (string)base[LoggingInstanceTypeName]; }

			set { base[LoggingInstanceTypeName] = value; }
		}

		private static object GetBusinessEntityType(string typeName)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

			foreach (var assembly in assemblies)
			{
				var t = assembly.GetType(typeName, false);
				if (t != null)
				{
					return Activator.CreateInstance(t);
				}
			}

			throw new ArgumentException("Type " + typeName + " doesn't exist in the current app domain");
		}

		public override Type BindingElementType
		{
			get { return typeof(SmevServiceMessageEncodingBindingElement); }
		}

		protected override BindingElement CreateBindingElement()
		{
			ILoggingSmevRequest instance = null;
			if (!string.IsNullOrEmpty(LoggingInstanceType))
			{
				instance = GetBusinessEntityType(LoggingInstanceType) as ILoggingSmevRequest;
			}

			var bindingElement =
				new SmevServiceMessageEncodingBindingElement(instance);
			ApplyConfiguration(bindingElement);
			return bindingElement;
		}
	}
}
