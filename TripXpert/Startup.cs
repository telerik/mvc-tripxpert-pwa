using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TripXpert.Startup))]
namespace TripXpert
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
