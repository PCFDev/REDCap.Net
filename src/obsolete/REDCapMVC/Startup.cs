using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(REDCapMVC.Startup))]
namespace REDCapMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
