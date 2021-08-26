using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace RestChild.Web.Controllers
{
	public class WindsorControllerFactory : DefaultControllerFactory
	{
		private readonly IKernel _kernel;

		public WindsorControllerFactory(IKernel kernel)
		{
			_kernel = kernel;
		}

		public virtual bool CanHandle(RequestContext request)
		{
			var controllerName = request.RouteData.Values["controller"] as string;
			Type controllerType = GetControllerType(request, controllerName);
			return _kernel.HasComponent(controllerType);
		}

		public override IController CreateController(RequestContext requestContext, string controllerName)
		{
			Type controllerType = GetControllerType(requestContext, controllerName);
			return (IController) _kernel.Resolve(controllerType);
		}

		public override void ReleaseController(IController controller)
		{
			_kernel.ReleaseComponent(controller);
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				throw new HttpException(404,
					string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
			}

			return (IController) _kernel.Resolve(controllerType);
		}
	}
}