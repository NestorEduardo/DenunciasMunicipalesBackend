using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DenunciasMunicipalesBackend.Startup))]
namespace DenunciasMunicipalesBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
