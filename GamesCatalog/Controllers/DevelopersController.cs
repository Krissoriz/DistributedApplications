using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameCatalog.Data.DBContexts;
using GameCatalog.Data.Entities;
using PagedList;

namespace GamesCatalog.Controllers
{
    public class DevelopersController : Controller
    {
        private VideoGameCatalogDBContext db = new VideoGameCatalogDBContext();

        // GET: Developers
        public ActionResult Index(string searchName, string searchFounder, int? page, String sortOrder)
        {
            int pageCurrent = page ?? 1;
            int pageMaxSize = 8;

            var developers = db.Developers.AsQueryable();
            ViewBag.FounderSearch = searchFounder;
            ViewBag.NameSearch = searchName;

            if (!string.IsNullOrEmpty(searchName))
                developers = developers.Where(x => x.Name.Contains(searchName));

            if (!string.IsNullOrEmpty(searchFounder))
                developers = developers.Where(x => x.Founder.Contains(searchFounder));

            ViewBag.SortOrder = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "title-desc" : "";
            ViewBag.FounderSortParam = string.IsNullOrEmpty(sortOrder) ? "title-desc" : "";

            switch (sortOrder)
            {
                case "title-desc":
                    developers = developers.OrderByDescending(x => x.Name);
                    break;
                case "rdate-asc":
                    developers = developers.OrderBy(x => x.Founder);
                    break;
                case "rdate-desc":
                    developers = developers.OrderByDescending(x => x.Founder);
                    break;
                default:
                    developers = developers.OrderBy(x => x.Name);
                    break;

            }

            return View(developers.ToPagedList(pageCurrent, pageMaxSize)); 
        }

        // GET: Developers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developers developers = db.Developers.Find(id);
            if (developers == null)
            {
                return HttpNotFound();
            }
            return View(developers);
        }

        // GET: Developers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Developers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Founder,Founded")] Developers developers)
        {
            if (ModelState.IsValid)
            {
                db.Developers.Add(developers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(developers);
        }

        // GET: Developers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developers developers = db.Developers.Find(id);
            if (developers == null)
            {
                return HttpNotFound();
            }
            return View(developers);
        }

        // POST: Developers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Founder,Founded")] Developers developers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(developers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(developers);
        }

        // GET: Developers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developers developers = db.Developers.Find(id);
            if (developers == null)
            {
                return HttpNotFound();
            }
            return View(developers);
        }

        // POST: Developers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Developers developers = db.Developers.Find(id);
            db.Developers.Remove(developers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
