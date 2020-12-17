using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Films.Startup))]
namespace Films
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
