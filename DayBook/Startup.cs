using DayBook.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            CreateRoles();
        }

        public void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("admin"))
            {
                var adminRole = new IdentityRole();
                adminRole.Name = "admin";
                roleManager.Create(adminRole);
            }
            if (!roleManager.RoleExists("user"))
            {
                var userRole = new IdentityRole();
                userRole.Name = "user";
                roleManager.Create(userRole);
            }

        }
    }
}
