using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YandexMapsOrganizationParser.Startup))]
namespace YandexMapsOrganizationParser
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
