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
using System.Web.Helpers;
using System.IO;
using NAudio.Wave;

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

        public async Task<ActionResult> ListPublishers() {
            return View(await db.Publishers.ToListAsync());
        }

        public async Task<ActionResult> ListPerformers() {
            return View(await Task.FromResult(db.Performers.ToList()));
        } 

        public ActionResult AddPerformer() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPerformer(Performer model) {
            model.Id = Guid.NewGuid().ToString();
            model.DateCreated = DateTime.Now;
            var img = WebImage.GetImageFromRequest("file");
            if(img != null) {
                string imgPath = @"~/Content/Image/PerformerImages/" + model.Id + "_" + Path.GetFileName(img.FileName);
                img.Save(imgPath);
                model.Thumbnail = imgPath.Replace("~", "");
            }
            if(ModelState.IsValid) {
                db.Performers.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("ListPerformers");
            }
            return View("Error");
        }

        public async Task<ActionResult> PerformerDetails(string id) {
            if(string.IsNullOrEmpty(id)) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(await db.Performers.Where(p => p.Id.Equals(id)).FirstOrDefaultAsync());
        }

        public async Task<ActionResult> EditPerformer(string id) {
            if(string.IsNullOrEmpty(id)) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(await db.Performers.Where(p => p.Id.Equals(id)).FirstOrDefaultAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPerformer(Performer model) {
            model.DateModified = DateTime.Now;
            var img = WebImage.GetImageFromRequest("file");
            if(img != null) {
                string imgPath = @"~/Content/Image/PerformerImages/" + model.Id + "_" + Path.GetFileName(img.FileName);
                img.Save(imgPath);
                model.Thumbnail = imgPath.Replace("~", "");
            }
            if(ModelState.IsValid) {
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("ListPerformers");
            }
            return View("Error"); 
        }

        public async Task<ActionResult> ListSongs() {
            return View(await Task.FromResult(db.Songs.Include(s => s.Album).ToList()));
        }

        public async Task<ActionResult> AddSong() {
            var model = new Song();
            model.Categories = await Task.FromResult(db.Categories.ToList());
            model.Performers = await Task.FromResult(db.Performers.ToList());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddSong(Song model, HttpPostedFileBase song) {
            model.Id = Guid.NewGuid().ToString();
            model.DateCreated = DateTime.Now;
            var img = WebImage.GetImageFromRequest("file");
            if(img != null) {
                string imgPath = @"~/Content/Image/SongImages/" + model.Id + "_" + Path.GetFileName(img.FileName);
                await Task.FromResult(img.Save(imgPath));
                model.Thumbnail = imgPath.Replace("~", "");
            }
            else {
                model.Thumbnail = @"/Content/Image/SongImages/song_default.png";
            }

            model.Link = @"/Content/UploadedSongs/" + model.Id + "_" + Path.GetFileName(song.FileName);
            model.Name = song.FileName;

            if(ModelState.IsValid) {
                song.SaveAs(Path.Combine(Server.MapPath("~/Content/UploadedSongs"), model.Id + "_" + Path.GetFileName(song.FileName)));
                model.Length = SongLength(Path.Combine(Server.MapPath("~/Content/UploadedSongs"), model.Id + "_" + Path.GetFileName(song.FileName))).ToString(@"mm\:ss");
                db.Songs.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("ListSongs");
            }
            return View("Error");
        }

        public async Task<ActionResult> SongDetails(string id) {
            if(string.IsNullOrEmpty(id)) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(await db.Songs.Where(p => p.Id.Equals(id)).FirstOrDefaultAsync());
        }
        
        private TimeSpan SongLength(string path) {
            Mp3FileReader song = new Mp3FileReader(path);
            return song.TotalTime;
        }
        [ChildActionOnly]
        public FileResult Download(string path, string name) {
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}