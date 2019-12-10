using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomerManager.Startup))]
namespace CustomerManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
