using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(my_website.Startup))]
namespace my_website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            ConfigureAuth(app);
            // Maping signalr MUST BE fater authorization, otherwise Context.User will always be null... 
            app.MapSignalR();
        }
    }
}
