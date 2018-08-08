using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HireProSol.Startup))]
namespace HireProSol
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
