namespace MusicStoreServer.Domain.Entities.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MusicStoreServer.Domain.Entities.Infrastructure.ApplicationDbContext>
    {
        // Enable-Migrations -ProjectName "MusicStoreServer.Domain.Entities" -StartUpProjectName "MusicStoreServer.Web" -Verbose

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MusicStoreServer.Domain.Entities.Infrastructure.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
