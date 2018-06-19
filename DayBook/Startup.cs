using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DayBook.Startup))]
namespace DayBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
