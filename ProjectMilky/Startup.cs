using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectMilky.Startup))]
namespace ProjectMilky
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
