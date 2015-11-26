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
using System.Net;

namespace Musicstore.Controllers {
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : BaseController {

        // GET: AdminPanel
        public ActionResult Index() {
            return View();
        }

        public ActionResult CreateRole() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRole(Role r) {
            if(ModelState.IsValid) {
                var role = new Role();
                role.Id = Guid.NewGuid().ToString();
                role.Name = r.Name;
                db.Roles.Add(role);
                await db.SaveChangesAsync();
                return View();
            }
            return View();
        }

        public async Task<ActionResult> ListUsers() {
            return View(await db.Users.ToListAsync());
        }

        public ActionResult UserDetails(string id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = UserManager.FindById(id);

            if(user == null) {
                return HttpNotFound();
            }

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
        
        public ActionResult EditUser(string id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = UserManager.FindById(id);

            if(user == null) {
                return HttpNotFound();
            }

            ViewBag.DefaultRole = UserManager.GetRoles(user.Id)[0];
            var roles = new SelectList(db.Roles.ToArray(), "Id", "Name");
            var editUser = new EditViewModel() {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = roles
            };

            return View(editUser);
        }

        [HttpPost, ActionName("EditUser")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUserConfirmed(EditViewModel model, string Role) {
            if(!ModelState.IsValid) {
                return RedirectToAction("ListUsers", "AdminPanel");
            }
            
            var user = UserManager.FindById(model.Id);
            if(user == null) {
                return HttpNotFound();
            }
            
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            UserManager.RemoveFromRoles(user.Id, UserManager.GetRoles(model.Id).ToArray());
            UserManager.AddToRole(user.Id, RoleManager.FindById(Role).Name);
            await UserManager.UpdateAsync(user);
            await db.SaveChangesAsync();
            return RedirectToAction("ListUsers", "AdminPanel");
        }

        public ActionResult DeleteUser(string id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = UserManager.FindById(id);
            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(string id) {
            if(!ModelState.IsValid) {
                return View();
            }

            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = UserManager.FindById(id);

            if(user == null) {
                return HttpNotFound();
            }

            UserManager.Delete(user);
            db.SaveChanges();
            return RedirectToAction("ListUsers", "AdminPanel");
        }
    }
}