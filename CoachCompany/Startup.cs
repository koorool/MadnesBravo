using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CoachCompany.Startup))]
namespace CoachCompany
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
