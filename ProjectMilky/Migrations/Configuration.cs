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
            
        }
    }
}
