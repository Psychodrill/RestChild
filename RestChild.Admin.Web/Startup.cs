using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestChild.Admin.Web.Startup))]
namespace RestChild.Admin.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
