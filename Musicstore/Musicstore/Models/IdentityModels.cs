using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Musicstore.Models {
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public bool IsActive { get; set; }
        //public string AccountName { get; set; }
    }

    public class Role : IdentityRole {
        public override string ToString() {
            return this.Name;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<User> {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) {
        }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Publisher>().ToTable("Publisher");
            modelBuilder.Entity<Song>().ToTable("Song");
            modelBuilder.Entity<Performer>().ToTable("Performer");
            modelBuilder.Entity<Album>().ToTable("Album");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Partner>().ToTable("Partner");
            modelBuilder.Entity<Api>().ToTable("Api");
        }

        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Api> Api { get; set; }
    }
}