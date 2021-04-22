using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StripeSQLRequestManager.Startup))]
namespace StripeSQLRequestManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
