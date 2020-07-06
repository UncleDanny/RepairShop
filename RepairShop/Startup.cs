using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RepairShop.Startup))]
namespace RepairShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
