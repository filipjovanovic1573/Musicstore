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
            context.Roles.AddOrUpdate(r => r.Name, new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = Guid.NewGuid().ToString(), Name = "User" },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin" });

            context.Categories.AddOrUpdate(c => c.Name,
                new Models.Category() { Id = Guid.NewGuid().ToString(), Name = "Rock", Image = "http://i.imgur.com/XO3DZVV.jpg", DateCreated = DateTime.Now },
                new Models.Category() { Id = Guid.NewGuid().ToString(), Name = "Hip-Hop", Image = "http://i.imgur.com/RYaSSs2.jpg", DateCreated = DateTime.Now },
                new Models.Category() { Id = Guid.NewGuid().ToString(), Name = "Pop", Image = "http://i.imgur.com/kvEyari.jpg", DateCreated = DateTime.Now },
                new Models.Category() { Id = Guid.NewGuid().ToString(), Name = "Dubstep", Image = "http://i.imgur.com/28jeHKi.jpg", DateCreated = DateTime.Now },
                new Models.Category() { Id = Guid.NewGuid().ToString(), Name = "House", Image = "http://i.imgur.com/SfE206z.jpg", DateCreated = DateTime.Now },
                new Models.Category() { Id = Guid.NewGuid().ToString(), Name = "Jazz", Image = "http://i.imgur.com/EWVevBD.jpg", DateCreated = DateTime.Now },
                new Models.Category() { Id = Guid.NewGuid().ToString(), Name = "Other", Image = "http://i.imgur.com/QmmgBU5.png", DateCreated = DateTime.Now });

            context.Performers.AddOrUpdate(c => c.Name, 
                new Models.Performer() { Id = Guid.NewGuid().ToString(), Name = "Unknown", NumberOfAlbums = 0, Thumbnail = "/Content/Image/PerformerImages/default_performer.png", DateCreated = DateTime.Now });
        }
    }
}
