using Microsoft.AspNet.Identity.Owin;
using Musicstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


//One controller to rule them all
namespace Musicstore.Controllers {
    public class BaseController : Controller {
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private ApplicationSignInManager signInManager;
        protected ApplicationDbContext db = new ApplicationDbContext();

        protected ApplicationUserManager UserManager {
            get {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                userManager = value;
            }
        }

        protected ApplicationRoleManager RoleManager {
            get {
                return roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set {
                roleManager = value;
            }
        }

        protected ApplicationSignInManager SignInManager {
            get {
                return signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set {
                signInManager = value;
            }
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                if(userManager != null) {
                    userManager.Dispose();
                    userManager = null;
                }

                if(signInManager != null) {
                    signInManager.Dispose();
                    signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}