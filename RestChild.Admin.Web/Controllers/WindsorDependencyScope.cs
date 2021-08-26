using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.Windsor;

namespace RestChild.Admin.Web.Controllers
{
	public class WindsorDependencyScope : IDependencyScope
	{
		private readonly IWindsorContainer container;

		private ConcurrentBag<object> toBeReleased = new ConcurrentBag<object>();

		public WindsorDependencyScope(IWindsorContainer container)
		{
			this.container = container;
		}

		public void Dispose()
		{
			if (this.toBeReleased != null)
			{
				foreach (var o in this.toBeReleased)
				{
					this.container.Release(o);
				}
			}

			this.toBeReleased = null;
		}

		public object GetService(Type serviceType)
		{
			if (!this.container.Kernel.HasComponent(serviceType))
			{
				return null;
			}

			var resolved = this.container.Resolve(serviceType);
			if (resolved != null)
			{
				this.toBeReleased.Add(resolved);
			}

			return resolved;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			if (!this.container.Kernel.HasComponent(serviceType))
			{
				return new object[0];
			}

			var allResolved = this.container.ResolveAll(serviceType).Cast<object>().ToList();
			allResolved.ForEach(x => this.toBeReleased.Add(x));
			return allResolved;
		}
	}
}
