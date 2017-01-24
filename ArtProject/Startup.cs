using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArtProject.Startup))]
namespace ArtProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
