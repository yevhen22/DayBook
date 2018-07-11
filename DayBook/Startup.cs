using DayBook.Content;
using DayBook.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

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
            try
            {

                if (!roleManager.RoleExists(ConstHelper.ADMINROLE))
                {
                    var adminRole = new IdentityRole();
                    adminRole.Name = ConstHelper.ADMINROLE;
                    roleManager.Create(adminRole);
                }
                if (!roleManager.RoleExists(ConstHelper.USERROLE))
                {
                    var userRole = new IdentityRole();
                    userRole.Name = ConstHelper.USERROLE;
                    roleManager.Create(userRole);
                }
            }
            catch (Exception exc)
            {

            }

        }
    }
}
