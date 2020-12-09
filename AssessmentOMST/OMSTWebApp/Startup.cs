using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OMSTWebApp.Startup))]
namespace OMSTWebApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
