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
            return View(await db.Songs.Where(s => s.Name.Contains(q) && s.IsActive == true).ToListAsync());
        }

        public async Task<ActionResult> FindCategory(string category) {
            if(category == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.SearchCategory = category;
            return View(await db.Songs.Include(s => s.Album).Where(s => s.Category.Name.Contains(category) && s.IsActive == true).ToListAsync());
        }

        public async Task<ActionResult> FindPerformer(string performer) {
            if(performer == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.SearchCategory = performer;
            return View(await db.Songs.Include(s => s.Album).Where(s => s.Performer.Name.Contains(performer) && s.IsActive == true).ToListAsync());
        }

        public async Task<ActionResult> FindAlbum(string album) {
            if(album == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.SearchString = album;
            return View(await db.Songs.Include(s => s.Album).Where(s => s.Album.AlbumName.Equals(album) && s.IsActive == true).ToListAsync());
        }
    }
}