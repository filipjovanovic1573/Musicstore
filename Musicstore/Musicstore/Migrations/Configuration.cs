namespace Musicstore.Migrations {
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Musicstore.Models.ApplicationDbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Musicstore.Models.ApplicationDbContext context) {
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

            context.Roles.AddOrUpdate(r => r.Name, new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = Guid.NewGuid().ToString(), Name = "User" },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin" });
            context.Categories.AddOrUpdate(c => c.Name,
                new Models.Category() { Id = Guid.NewGuid().ToString(), Name = "Rock", Image = "http://i.imgur.com/XO3DZVV.jpg", DateCreated = DateTime.Now },
                new Models.Category() { Id = Guid.NewGuid().ToString(), Name = "Hip-Hop", Image = "http://i.imgur.com/RYaSSs2.jpg", DateCreated = DateTime.Now },
                new Models.Category() { Id = Guid.NewGuid().ToString(), Name = "Pop", Image = "http://i.imgur.com/kvEyari.jpg", DateCreated = DateTime.Now },
                new Models.Category() { Id = Guid.NewGuid().ToString(), Name = "Dubstep", Image = "http://i.imgur.com/28jeHKi.jpg", DateCreated = DateTime.Now },
                new Models.Category() { Id = Guid.NewGuid().ToString(), Name = "House", Image = "http://i.imgur.com/SfE206z.jpg", DateCreated = DateTime.Now },
                new Models.Category() { Id = Guid.NewGuid().ToString(), Name = "Jazz", Image = "http://i.imgur.com/EWVevBD.jpg", DateCreated = DateTime.Now });
        }
    }
}
