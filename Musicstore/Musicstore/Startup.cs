using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Musicstore.Startup))]
namespace Musicstore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
