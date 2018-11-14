using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DinnergeddonWeb.Startup))]
namespace DinnergeddonWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
