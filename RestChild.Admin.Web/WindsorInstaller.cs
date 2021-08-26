using RestChild.Admin.Web.Common;
using RestChild.Extensions.Services;

namespace RestChild.Admin.Web
{
   using Castle.DynamicProxy;
   using Castle.MicroKernel.Registration;
   using Castle.MicroKernel.Resolvers;
   using Castle.MicroKernel.SubSystems.Configuration;
   using Castle.Windsor;
   using RestChild.Admin.Web.Controllers;

   public class WindsorInstaller : IWindsorInstaller
   {
      /// <summary>
      ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
      /// </summary>
      /// <param name="container">The container.</param>
      /// <param name="store">The configuration store.</param>
      public void Install(IWindsorContainer container, IConfigurationStore store)
      {
         container.Register(Classes.FromThisAssembly().BasedOn(typeof(IRequestSaver)).WithServiceAllInterfaces()
            .WithServiceSelf().LifestyleTransient());

         container.Register(
            Component.For<ILazyComponentLoader>().ImplementedBy<LazyOfTComponentLoader>(),
            Classes.FromThisAssembly().BasedOn(typeof(ILogic))
               .LifestyleTransient().Configure(c => c.Interceptors<LogAspect>()));


         container.Register(Component.For<IInterceptor>().ImplementedBy<LogAspect>());

      }
   }
}
