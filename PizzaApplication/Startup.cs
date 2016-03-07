using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PizzaApplication.Startup))]
namespace PizzaApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
