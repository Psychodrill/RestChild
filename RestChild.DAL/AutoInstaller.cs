using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RestChild.Comon;

namespace RestChild.DAL
{
	public class AutoInstaller : IWindsorInstaller
	{
		/// <summary>
		/// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
		/// </summary>
		/// <param name="container">The container.</param><param name="store">The configuration store.</param>
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
            container.Register(Classes.FromAssemblyContaining(typeof(UnitOfWork)).BasedOn<IUnitOfWork>().WithServiceAllInterfaces().LifestyleTransient());
		}
	}
}
