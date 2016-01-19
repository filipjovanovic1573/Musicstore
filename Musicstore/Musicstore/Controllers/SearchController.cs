using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Musicstore.Controllers {
    public class SearchController : BaseController {
        // GET: Search
        public ActionResult Index() {
            return View();
        }

        public async Task<ActionResult> FindSong(string q) {
            if(q == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.SearchString = q;
            return View(await db.Songs.Where(s => s.Name.Contains(q)).ToListAsync());
        }

        public async Task<ActionResult> FindCategory(string q) {
            if(q == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.SearchCategory = q;
            return View(await db.Songs.Include(s => s.Album).Where(s => s.Category.Name.Contains(q)).ToListAsync());
        }

        public async Task<ActionResult> FindPerformer(string q) {
            if(q == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.SearchCategory = q;
            return View(await db.Songs.Include(s => s.Album).Where(s => s.Performer.Name.Contains(q)).ToListAsync());
        }
    }
}