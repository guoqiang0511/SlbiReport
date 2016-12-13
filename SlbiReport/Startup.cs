using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SlbiReport.Startup))]
namespace SlbiReport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

    }
}
