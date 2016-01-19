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
            model.Categories = await Task.FromResult(db.Categories.ToList());
            model.Performers = await Task.FromResult(db.Performers.ToList());
            model.Albums = await Task.FromResult(db.Albums.ToList());
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

        public ActionResult PartnerList() {
            return View();
        }

        public async Task<ActionResult> AllCategories() {
            return View(await Task.FromResult(db.Categories.ToList()));
        }
        public async Task<ActionResult> AllPerformers() {
            return View(await Task.FromResult(db.Performers.ToList()));
        }
        public async Task<ActionResult> AllAlbums() {
            return View(await Task.FromResult(db.Albums.ToList()));
        }

        public async Task<ActionResult> SongDetails(string id) {
            if(string.IsNullOrEmpty(id)) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(await db.Songs.Where(p => p.Id.Equals(id)).FirstOrDefaultAsync());
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}