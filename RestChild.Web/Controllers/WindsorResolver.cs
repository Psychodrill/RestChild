using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.Windsor;

namespace RestChild.Web.Controllers
{
	public class WindsorResolver : IDependencyResolver
	{
		private readonly IWindsorContainer _container;
		private ConcurrentBag<object> _toBeReleased = new ConcurrentBag<object>();

		public WindsorResolver(IWindsorContainer container)
		{
			_container = container;
		}

		public IDependencyScope BeginScope()
		{
			return new WindsorDependencyScope(_container);
		}

		public void Dispose()
		{
			foreach (object o in _toBeReleased)
			{
				_container.Release(o);
			}

			_toBeReleased = new ConcurrentBag<object>();
		}

		public object GetService(Type serviceType)
		{
			if (!_container.Kernel.HasComponent(serviceType))
			{
				return null;
			}

			object resolved = _container.Resolve(serviceType);
			if (resolved != null)
			{
				_toBeReleased.Add(resolved);
			}

			return resolved;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			if (!_container.Kernel.HasComponent(serviceType))
			{
				return new object[0];
			}

			List<object> allResolved = _container.ResolveAll(serviceType).Cast<object>().ToList();
			allResolved.ForEach(x => _toBeReleased.Add(x));
			return allResolved;
		}
	}
}