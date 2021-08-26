using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RestChild.Extensions.Services;
using RestChild.Mobile.DAL;
using RestChild.Web.Controllers;
using RestChild.Web.Logic;

namespace RestChild.Web
{
    public class WindsorInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn(typeof(IRequestSaver)).WithServiceAllInterfaces()
                .WithServiceSelf().LifestyleTransient());

            container.Register(
                Component.For<ILazyComponentLoader>().ImplementedBy<LazyOfTComponentLoader>(),
                Classes.FromThisAssembly().BasedOn(typeof(ILogic))
                    .LifestyleTransient().Configure(c => c.Interceptors<LogAspect>()));

            container.Register(Component.For<IInterceptor>().ImplementedBy<LogAspect>());

            container.Register(Component.For<AnalyticReportFilterRepository>().LifestyleSingleton());
        }
    }
}
