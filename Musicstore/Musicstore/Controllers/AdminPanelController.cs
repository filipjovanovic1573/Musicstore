using Microsoft.AspNet.Identity.EntityFramework;
using Musicstore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Musicstore.ViewModels;

namespace Musicstore.Controllers {
    public class AdminPanelController : Controller {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager userManager;
        public ApplicationUserManager UserManager {
            get {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                userManager = value;
            }
        }
        
        // GET: AdminPanel
        public ActionResult Index() {
            return View();
        }

        public async Task<ActionResult> ListUsers() {
            return View(await db.Users.ToListAsync());
        }

        public ActionResult UserDetails(string id) {
            var user = UserManager.FindById(id);
            var roles = UserManager.GetRoles(id).ToArray();
            var detailsUser = new DetailsViewModel {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEndDate = user.LockoutEndDateUtc,
                TwoFactor = user.TwoFactorEnabled,
                Roles = roles
            };
            return View(detailsUser);
        }
    }
}