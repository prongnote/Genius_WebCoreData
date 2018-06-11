using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Genius_WebCoreData.Startup))]
namespace Genius_WebCoreData
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
