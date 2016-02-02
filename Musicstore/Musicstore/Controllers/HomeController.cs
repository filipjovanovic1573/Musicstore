using Microsoft.AspNet.Identity;
using Musicstore.Models;
using Musicstore.Other;
using Musicstore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Musicstore.Controllers {
    public class HomeController : BaseController {
        public async Task<ActionResult> Index() {
            var model = new HomeViewModel();
            model.Categories = await Task.FromResult(db.Categories.Take(3).ToList());
            model.Performers = await Task.FromResult(db.Performers.Where(p => !p.Name.Equals("Unknown") && p.IsActive == true).Take(3).ToList());
            model.Albums = await Task.FromResult(db.Albums.Take(3).ToList());
            return View(model);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PartnerProgram() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PartnerProgram(Partner model) {
            model.DateCreated = DateTime.Now;
            model.Approved = false;
            model.Id = Guid.NewGuid().ToString();
            if(ModelState.IsValid) {
                db.Partners.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Error");
        }

        public ActionResult PartnerList() {
            return View();
        }

        public async Task<ActionResult> AllCategories() {
            var list = await Task.FromResult(db.Categories.ToList());
            var item = list.Where(p => p.Name.Equals("Other")).FirstOrDefault();
            list = list.OrderBy(p => p.Name).ToList();
            list.Remove(item);
            list.Insert(list.Count, item);
            return View(list);
        }

        public async Task<ActionResult> AllPerformers() {
            return View(await Task.FromResult(db.Performers.Where(p => p.IsActive == true).ToList()));
        }

        public async Task<ActionResult> AllAlbums() {
            return View(await Task.FromResult(db.Albums.Where(p => p.IsActive == true).ToList()));
        }

        public async Task<ActionResult> AllPublishers() {
            return View(await Task.FromResult(db.Publishers.Where(p => p.IsActive == true).ToList()));
        }

        public async Task<ActionResult> AllPartners() {
            return View(await db.Partners.Where(p => p.Approved == true).ToListAsync());
        }

        public async Task<ActionResult> SongDetails(string id) {
            if(string.IsNullOrEmpty(id)) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(await db.Songs.Where(p => p.Id.Equals(id)).FirstOrDefaultAsync());
        }

        [Authorize]
        public ActionResult GetApi() {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetApi(Api api) {
            //x3vkcdlwf2u
            api.Id = Guid.NewGuid().ToString();
            api.UserId = User.Identity.GetUserId();
            api.Key = ApiKey.GetKey();
            if(ModelState.IsValid) {
                db.Api.Add(api);
                await db.SaveChangesAsync();
            }
            ViewBag.ApiKey = api.Key;
            return View();
        }
        
        protected override void Dispose(bool disposing) {
            if(disposing) {
                db.Dispose();
                UserManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}