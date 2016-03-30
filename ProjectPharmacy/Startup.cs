using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectPharmacy.Startup))]
namespace ProjectPharmacy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
