using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Musicstore.Models;

namespace Musicstore.Controllers {
    public class PublisherController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Publisher
        public async Task<ActionResult> Index() {
            return View(await db.Publishers.ToListAsync());
        }

        // GET: Publisher/Details/5
        public async Task<ActionResult> Details(string id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = await db.Publishers.FindAsync(id);
            if(publisher == null) {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // GET: Publisher/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Publisher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Email,Phone,Address")] Publisher publisher) {
            if(ModelState.IsValid) {
                publisher.Id = Guid.NewGuid().ToString();
                db.Publishers.Add(publisher);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(publisher);
        }

        // GET: Publisher/Edit/5
        public async Task<ActionResult> Edit(string id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = await db.Publishers.FindAsync(id);
            if(publisher == null) {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // POST: Publisher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Email,Phone,Address")] Publisher publisher) {
            if(ModelState.IsValid) {
                db.Entry(publisher).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(publisher);
        }

        // GET: Publisher/Delete/5
        public async Task<ActionResult> Delete(string id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = await db.Publishers.FindAsync(id);
            if(publisher == null) {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // POST: Publisher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id) {
            Publisher publisher = await db.Publishers.FindAsync(id);
            db.Publishers.Remove(publisher);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
