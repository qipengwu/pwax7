using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(pwax7mobileappService.Startup))]

namespace pwax7mobileappService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}