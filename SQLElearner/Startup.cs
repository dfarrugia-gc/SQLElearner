using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SQLElearner.Startup))]
namespace SQLElearner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
