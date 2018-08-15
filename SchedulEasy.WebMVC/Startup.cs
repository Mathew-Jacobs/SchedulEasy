using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchedulEasy.WebMVC.Startup))]
namespace SchedulEasy.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
