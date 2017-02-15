using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StackUndertow_MVC.Startup))]
namespace StackUndertow_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
