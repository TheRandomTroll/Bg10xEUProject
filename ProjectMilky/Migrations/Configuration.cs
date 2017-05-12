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
            AutomaticMigrationDataLossAllowed = true;
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
                roleManager.Create(new IdentityRole("Administrator"));
            }
            var school1 = new School { Name = "Technological School \'Electronic Systems\'" };
            var school2 = new School { Name = "National High School of Mathematics \'Acad. L. Tchakalov\'" };
            var school3 = new School { Name = "Sofia High School of Mathematics" };
            if (!context.Schools.Any())
            {
                context.Schools.AddOrUpdate(school1, school2, school3);
            }
            var subj1 = new Subject
            {
                Name = "Maths"
            };
            var subj2 = new Subject
            {
                Name = "Physics"
            };
            var subj3 = new Subject
            {
                Name = "English"
            };
            var subj4 = new Subject
            {
                Name = "History"
            };
            var subj5 = new Subject
            {
                Name = "Informational Technologies"
            };

            if (!context.Subjects.Any())
            {
                context.Subjects.AddOrUpdate(subj1, subj2, subj3, subj4, subj5);
            }
        }
    }
}
