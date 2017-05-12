namespace ProjectMilky.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using ProjectMilky.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectMilky.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ProjectMilky.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            if (!roleStore.Roles.Any())
            {
                roleManager.Create(new IdentityRole("Student"));
                roleManager.Create(new IdentityRole("Teacher"));
                roleManager.Create(new IdentityRole("Principal"));
                roleManager.Create(new IdentityRole("Administrator"));
            }
            if (!userManager.Users.Any())
            {
                
            }
        }
    }
}
