using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FujitsuFinal.Startup))]
namespace FujitsuFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
