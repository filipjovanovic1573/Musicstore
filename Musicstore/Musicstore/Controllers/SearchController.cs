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
            ViewBag.SearchString = q;
            return View(await db.Songs.Where(s => s.Name.Contains(q)).ToListAsync());
        }
    }
}