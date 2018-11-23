using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentMonitoringSystem.Startup))]
namespace StudentMonitoringSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
