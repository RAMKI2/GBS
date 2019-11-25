using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JPSMVC.Startup))]
namespace JPSMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
