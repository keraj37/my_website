using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(my_website.Startup))]
namespace my_website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
