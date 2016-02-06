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
using System.Net.Mail;
using System.Globalization;

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
                Roles = roles,
                IsActive = user.IsActive
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
            user.IsActive = model.IsActive;
            UserManager.RemoveFromRoles(user.Id, UserManager.GetRoles(model.Id).ToArray());
            UserManager.AddToRole(user.Id, RoleManager.FindById(Role).Name);
            await UserManager.UpdateAsync(user);
            await db.SaveChangesAsync();
            return RedirectToAction("ListUsers", "AdminPanel");
        }

        public async Task<ActionResult> ListPublishers() {
            return View(await db.Publishers.ToListAsync());
        }

        public async Task<ActionResult> PublisherDetails(string id) {
            if(string.IsNullOrEmpty(id)) {
                return View("Error");
            }
            return View(await db.Publishers.Where(p => p.Id.Equals(id)).FirstOrDefaultAsync());
        }

        public async Task<ActionResult> ListPerformers() {
            var performers = await Task.FromResult(db.Performers.ToList());
            performers.ForEach(p => p.NumberOfAlbums = db.Albums.AsNoTracking().Where(s => s.PerformerId.Equals(p.Id)).Count());
            return View(performers);
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
            else {
                model.Thumbnail = "/Content/Image/PerformerImages/default_performer.png";
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
            var performer = await db.Performers.Where(p => p.Id.Equals(id)).FirstOrDefaultAsync();
            performer.NumberOfAlbums = await db.Albums.AsNoTracking().Where(p => p.PerformerId.Equals(performer.Id)).CountAsync();
            return View(performer);
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

        public async Task<ActionResult> EditPublisher(string id) {
            if(string.IsNullOrEmpty(id)) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(await db.Publishers.Where(p => p.Id.Equals(id)).FirstOrDefaultAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPublisher(Publisher model) {
            model.DateModified = DateTime.Now;
            var img = WebImage.GetImageFromRequest("file");
            if(img != null) {
                string imgPath = @"~/Content/Image/PublisherImages/" + model.Id + "_" + Path.GetFileName(img.FileName);
                img.Save(imgPath);
                model.Thumbnail = imgPath.Replace("~", "");
            }
            if(ModelState.IsValid) {
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("ListPublishers");
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
            var img = WebImage.GetImageFromRequest("img");
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

        public async Task<ActionResult> EditSong(string id) {
            if(string.IsNullOrEmpty(id)) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = await db.Songs.Where(p => p.Id.Equals(id)).FirstOrDefaultAsync();
            model.Categories = await Task.FromResult(db.Categories.ToList());
            model.Performers = await Task.FromResult(db.Performers.ToList());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSong(Song model) {
            model.DateModified = DateTime.Now;
            var img = WebImage.GetImageFromRequest("file");
            if(img != null) {
                string imgPath = @"~/Content/Image/SongImages/" + model.Id + "_" + Path.GetFileName(img.FileName);
                img.Save(imgPath);
                model.Thumbnail = imgPath.Replace("~", "");
            }
            else {
                model.Thumbnail = (await db.Songs.Where(s => s.Id.Equals(model.Id)).AsNoTracking().FirstOrDefaultAsync()).Thumbnail;
            }

            if(ModelState.IsValid) {
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("ListSongs");
            }
            return View("Error");

        }

        public async Task<ActionResult> ListPartners() {
            return View(await db.Partners.ToListAsync());
        }

        public async Task<ActionResult> SongDetails(string id) {
            if(string.IsNullOrEmpty(id)) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(await db.Songs.Where(p => p.Id.Equals(id)).FirstOrDefaultAsync());
        }


        public ActionResult AddPublisher() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPublisher(Publisher model) {
            model.Id = Guid.NewGuid().ToString();
            model.DateCreated = DateTime.Now;

            var img = WebImage.GetImageFromRequest("file");
            if(img != null) {
                string imgPath = @"~/Content/Image/PublisherImages/" + model.Id + "_" + Path.GetFileName(img.FileName);
                img.Save(imgPath);
                model.Thumbnail = imgPath.Replace("~", "");
            }
            else {
                model.Thumbnail = "/Content/Image/PublisherImages/publisher_default.png";
            }

            if(ModelState.IsValid) {
                db.Publishers.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("ListPublishers");
            }

            return View("Error");
        }


        public async Task<ActionResult> ListAlbums() {
            var models = await db.Albums.ToArrayAsync();

            for(int i = 0; i < models.Count(); i++) {
                string id = models.ElementAt(i).Id;
                models.ElementAt(i).NumberOfSongs = db.Songs.Where(s => s.AlbumId.Equals(id)).Count();
            }

            return View(models);
        }

        public async Task<ActionResult> AddAlbum() {
            var model = new Album();
            model.Publishers = await db.Publishers.ToListAsync();
            model.Performers = await db.Performers.ToListAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAlbum(Album model) {
            model.Id = Guid.NewGuid().ToString();
            var list = Request["filteredsongs"].Split(',').ToList();
            model.NumberOfSongs = list == null ? 0 : list.Count;
            List<Song> songList = new List<Song>();
            if(list.Count > 0) {
                foreach(var item in list) {
                    if(string.IsNullOrEmpty(item) || item != "0") {
                        var song = await db.Songs.Where(s => s.Id.Equals(item)).FirstOrDefaultAsync();
                        song.AlbumId = model.Id;
                        songList.Add(song);
                    }
                }
            }

            model.DateCreated = DateTime.Now;
            model.Songs = songList;
            var image = WebImage.GetImageFromRequest("file");
            string path = "";
            if(image != null) {
                path = Path.Combine(Server.MapPath("/Content/Image/AlbumImages/"), model.Id + "_" + Path.GetFileName(image.FileName));
                image.Save(path);
                model.Thumbnail = "/Content/Image/AlbumImages/" + model.Id + "_" + Path.GetFileName(image.FileName);
            }
            else {
                model.Thumbnail = "/Content/Image/AlbumImages/album_default.png";
            }

            if(ModelState.IsValid) {
                db.Albums.Add(model);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("ListAlbums");
        }

        public async Task<ActionResult> AlbumDetails(string id) {
            if(string.IsNullOrEmpty(id)) {
                return View("Error");
            }

            var model = await db.Albums.Where(a => a.Id.Equals(id)).FirstOrDefaultAsync();
            model.SongsSL = await db.Songs.Where(s => s.AlbumId.Equals(id)).ToListAsync();
            model.NumberOfSongs = model.SongsSL.Count();
            return View(model);
        }

        public async Task<ActionResult> EditAlbum(string id) {
            if(string.IsNullOrEmpty(id)) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = await db.Albums.Where(a => a.Id.Equals(id)).FirstOrDefaultAsync();
            model.Publishers = await db.Publishers.ToListAsync();
            model.Performers = await db.Performers.ToListAsync();
            model.SongsSL = await db.Songs.Where(s => s.PerformerId.Equals(model.PerformerId)).ToListAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAlbum(Album model) {
            var list = Request["filteredsongs"].Split(',').ToList();
            model.DateModified = DateTime.Now;
            model.NumberOfSongs = list == null ? 0 : list.Count;
            List<Song> songList = new List<Song>();
            if(list.Count > 0) {
                foreach(var item in list) {
                    if(string.IsNullOrEmpty(item) || item != "0") {
                        var song = await db.Songs.Where(s => s.Id.Equals(item)).FirstOrDefaultAsync();
                        song.AlbumId = model.Id;
                        songList.Add(song);
                    }
                }
            }

            var img = WebImage.GetImageFromRequest("file");
            if(img != null) {
                string imgPath = @"~/Content/Image/AlbumImages/" + model.Id + "_" + Path.GetFileName(img.FileName);
                img.Save(imgPath);
                model.Thumbnail = imgPath.Replace("~", "");
            }
            else {
                model.Thumbnail = "/Content/Image/AlbumImages/album_default.png";
            }

            if(ModelState.IsValid) {
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("ListAlbums");
            }
            return View("Error");
        }


        //HELPER METHODS
        private TimeSpan SongLength(string path) {
            Mp3FileReader song = new Mp3FileReader(path);
            return song.TotalTime;
        }
        
        public FileResult Download(string path, string name) {
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }
        [HttpPost]
        public JsonResult FilterSongs(string id) {
            MultiSelectList list;
            //if(string.IsNullOrEmpty(id) || id.Equals("39deb552-6977-4108-ad2d-06c8c8de82b4")) {
            //    list = new SelectList(db.Songs.ToList(), "Id", "Name", 0);
            //}
            //else {
            //    list = new SelectList(db.Songs.Where(s => s.Performer.Name.Equals(id)).ToList(), "Id", "Name", 0);
            //}
            list = new MultiSelectList(db.Songs.Where(s => s.Performer.Id.Equals(id)).ToList(), "Id", "Name");
            return Json(list);
        }

        [HttpPost]
        public ActionResult Approve(string id) {
            var model = db.Partners.Where(p => p.Id.Equals(id)).FirstOrDefault();
            if(model.Approved == false) {
                model.Approved = true;
            }
            model.DateModified = DateTime.Now;
            db.Entry(model).State = EntityState.Modified;
            int r = db.SaveChanges();
            if(r == 1) {
                SendEmail("Aww yisss", "You've been accepted in our partner program. Congrats!!!", model.Email);
            }
            return RedirectToAction("ListPartners");
        }

        public ActionResult Disapprove(string id) {
            var model = db.Partners.Where(p => p.Id.Equals(id)).FirstOrDefault();
            if(model.Approved == true) {
                model.Approved = false;
            }
            model.DateModified = DateTime.Now;
            db.Entry(model).State = EntityState.Modified;
            int r = db.SaveChanges();
            if(r == 1) {
                SendEmail("Bad news", "You've been removed from our partner program. It's a sad day...", model.Email);
            }
            return RedirectToAction("ListPartners");
        }

        private void SendEmail(string subject, string text, string destination) {
            int port = int.Parse(Properties.Resources.smtpPort);

            var smtp = new SmtpClient(Properties.Resources.sendgridSmtp, port);

            var credentials = new NetworkCredential(Properties.Resources.mailAccount, Properties.Resources.mailPassword);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credentials;
            smtp.EnableSsl = false;

            var to = new MailAddress(destination);
            var from = new MailAddress("fil.93@hotmail.com", "support@musicstore.com");

            var msg = new MailMessage();

            msg.To.Add(to);
            msg.From = from;
            msg.IsBodyHtml = true;
            msg.Subject = subject;
            msg.Body = text;
            smtp.Send(msg);
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}