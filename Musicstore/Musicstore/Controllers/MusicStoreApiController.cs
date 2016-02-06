using Musicstore.Models;
using Musicstore.Other;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Musicstore.Controllers {
    public class MusicStoreApiController : ApiController {

        private ApplicationDbContext db = new ApplicationDbContext();

        #region Category
        [Route("api/{key}/MusicStoreApi/GetCategories")]
        public async Task<IHttpActionResult> GetCategories(string key) {
            if(db.Api.Where(a => a.Key.Equals(key)).FirstOrDefault() == null) {
                return Json(new ErrorMessage() { Error = "Authentication failed" });
            }
            return Json(await db.Categories.ToListAsync());
        }
        #endregion

        #region Search
        [HttpGet]
        [Route("api/{key}/MusicStoreApi/GetByName/{q:alpha}")]
        public async Task<IHttpActionResult> GetByName(string q, string key) {
            if(db.Api.Where(a => a.Key.Equals(key)).FirstOrDefault() == null) {
                return Json(new ErrorMessage() { Error = "Authentication failed" });
            }

            if(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)) {
                return Json(new ErrorMessage() { Error = "Bad request" });
            }

            return Json(await db.Songs.Where(s => s.Name.Contains(q) && s.IsActive == true).ToListAsync());
        }

        [HttpGet]
        [Route("api/{key}/MusicStoreApi/GetByCategory/{q:alpha}")]
        public async Task<IHttpActionResult> GetByCategory(string q, string key) {
            if(db.Api.Where(a => a.Key.Equals(key)).FirstOrDefault() == null) {
                return Json(new ErrorMessage() { Error = "Authentication failed" });
            }

            if(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)) {
                return Json(new ErrorMessage() { Error = "Bad request" });
            }

            return Json(await db.Songs.Where(s => s.Category.Name.Contains(q) && s.IsActive == true).ToListAsync());
        }

        [HttpGet]
        [Route("api/{key}/MusicStoreApi/GetByAlbum/{q}")]
        public async Task<IHttpActionResult> GetByAlbum(string q, string key) {
            if(db.Api.Where(a => a.Key.Equals(key)).FirstOrDefault() == null) {
                return Json(new ErrorMessage() { Error = "Authentication failed" });
            }

            if(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)) {
                return Json(new ErrorMessage() { Error = "Bad request" });
            }

            return Json(await db.Songs.Where(s => s.Album.AlbumName.Contains(q) && s.IsActive == true).ToListAsync());
        }

        [HttpGet]
        [Route("api/{key}/MusicStoreApi/GetByPerformer/{q}")]
        public async Task<IHttpActionResult> GetByPerformer(string q, string key) {
            if(db.Api.Where(a => a.Key.Equals(key)).FirstOrDefault() == null) {
                return Json(new ErrorMessage() { Error = "Authentication failed" });
            }

            if(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)) {
                return Json(new ErrorMessage() { Error = "Bad request" });
            }

            return Json(await db.Songs.Where(s => s.Performer.Name.Contains(q) && s.IsActive == true).ToListAsync());
        }
        #endregion

        #region Song
        [HttpGet]
        [Route("api/{key}/MusicStoreApi/GetSongs/")]
        public async Task<IHttpActionResult> GetSongs(string key) {
            if(db.Api.Where(a => a.Key.Equals(key)).FirstOrDefault() == null) {
                return Json(new ErrorMessage() { Error = "Authentication failed" });
            }
            return Json(await (from s in db.Songs.Include(o => o.Album).Include(o => o.Performer) select s).ToListAsync());
        }


        [HttpGet]
        [Route("api/{key}/MusicStoreApi/GetById/{id}")]
        // GET api/<controller>/5
        public async Task<IHttpActionResult> GetById(string id, string key) {
            if(db.Api.Where(a => a.Key.Equals(key)).FirstOrDefault() == null) {
                return Json(new ErrorMessage() { Error = "Authentication failed" });
            }

            if(string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id)) {
                return Json(new ErrorMessage() { Error = "Bad request" });
            }

            return Json(await (from s in db.Songs where s.Id.Equals(id) select s).FirstOrDefaultAsync());
        }
        #endregion

        #region Album
        [HttpGet]
        [Route("api/{key}/MusicStoreApi/GetAlbums/")]
        public async Task<IHttpActionResult> GetAlbums(string key) {
            if(db.Api.Where(a => a.Key.Equals(key)).FirstOrDefault() == null) {
                return Json(new ErrorMessage() { Error = "Authentication failed" });
            }

            return Json(await db.Albums.ToListAsync());
        }
        #endregion

        #region Performers
        [HttpGet]
        [Route("api/{key}/MusicStoreApi/GetPerformers/")]
        public async Task<IHttpActionResult> GetPerformers(string key) {
            if(db.Api.Where(a => a.Key.Equals(key)).FirstOrDefault() == null) {
                return Json(new ErrorMessage() { Error = "Authentication failed" });
            }

            return Json(await db.Performers.ToListAsync());
        }
        #endregion

        protected override void Dispose(bool disposing) {
            if(disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
