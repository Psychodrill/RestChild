using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace RestChild.Comon
{
    /// <summary>
    ///     Wrapper class for IoC container
    /// </summary>
    public class WindsorHolder
    {
        private static readonly Lazy<WindsorHolder> Lazy = new Lazy<WindsorHolder>(() => new WindsorHolder());

        private WindsorHolder()
        {
            LoadAssemblies(ApplicationBinPath, "*.dll");

            Container = new WindsorContainer();

            Container.AddFacility<LoggingFacility>(f => f.UseLog4Net());

            Container.Install(Configuration.FromAppConfig());

            var windsorInstallers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(a => typeof(IWindsorInstaller).IsAssignableFrom(a) && !a.FullName.StartsWith("Castle"));

            var fromReflection =
                windsorInstallers.ToArray().Select(a => Activator.CreateInstance(a) as IWindsorInstaller).ToArray();

            Container.Install(fromReflection);
        }

        private static string ApplicationBinPath
        {
            get
            {
                var u = new Uri(typeof(WindsorHolder).Assembly.CodeBase);
                return Path.GetDirectoryName(Uri.UnescapeDataString(u.AbsolutePath));
            }
        }

        /// <summary>
        ///     Instance
        /// </summary>
        public static WindsorHolder Instance => Lazy.Value;

        /// <summary>
        ///     Container
        /// </summary>
        public WindsorContainer Container { get; }

        private static void LoadAssemblies(string pluginPath, string pattern)
        {
            if (!Directory.Exists(pluginPath))
            {
                return;
            }

            foreach (var path in Directory.GetFiles(pluginPath, pattern))
            {
                try
                {
                    var asmName = AssemblyName.GetAssemblyName(path);
                    if (!AppDomain.CurrentDomain.GetAssemblies().Any(a => a.GetName() == asmName))
                    {
                        AppDomain.CurrentDomain.Load(asmName);
                    }
                }
                catch
                {
                }
            }
        }

        public static void Dispose()
        {
            Instance.Container.Dispose();
        }

        public static void Release(object instance)
        {
            Instance.Container.Release(instance);
        }

        public static object Resolve(Type service)
        {
            return Instance.Container.Resolve(service);
        }

        public static object Resolve(Type service, IDictionary arguments)
        {
            return Instance.Container.Resolve(service, arguments);
        }

        public static object Resolve(Type service, object argumentsAsAnonimousType)
        {
            return Instance.Container.Resolve(service, argumentsAsAnonimousType);
        }

        public static object Resolve(string key, Type service)
        {
            return Instance.Container.Resolve(key, service);
        }

        public static object Resolve(string key, Type service, IDictionary arguments)
        {
            return Instance.Container.Resolve(key, service, arguments);
        }

        public static object Resolve(string key, Type service, object argumentsAsAnonimousType)
        {
            return Instance.Container.Resolve(key, service, argumentsAsAnonimousType);
        }

        public static T Resolve<T>()
        {
            return Instance.Container.Resolve<T>();
        }

        public static T Resolve<T>(IDictionary arguments)
        {
            return Instance.Container.Resolve<T>(arguments);
        }

        public static T Resolve<T>(object argumentsAsAnonimousType)
        {
            return Instance.Container.Resolve<T>(argumentsAsAnonimousType);
        }

        public static T Resolve<T>(string key)
        {
            return Instance.Container.Resolve<T>(key);
        }

        public static T Resolve<T>(string key, IDictionary arguments)
        {
            return Instance.Container.Resolve<T>(key, arguments);
        }

        public static T Resolve<T>(string key, object argumentsAsAnonimousType)
        {
            return Instance.Container.Resolve<T>(key, argumentsAsAnonimousType);
        }
    }
}
