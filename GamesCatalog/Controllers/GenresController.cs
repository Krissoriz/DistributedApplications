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
    public class GenresController : Controller
    {
        private VideoGameCatalogDBContext db = new VideoGameCatalogDBContext();

        // GET: Genres
        public ActionResult Index(String searchGenre, int? page, String sortOrder)
        {
            int pageCurrent = page ?? 1;
            int pageMaxSize = 8;

            var genres = db.Genres.AsQueryable();
            ViewBag.GenreSearch = searchGenre;

            if (!string.IsNullOrEmpty(searchGenre))
                genres = genres.Where(x => x.Value.Contains(searchGenre));

            ViewBag.SortOrder = sortOrder;
            ViewBag.GenreSortParam = string.IsNullOrEmpty(sortOrder) ? "title-desc" : "";

            switch (sortOrder)
            {
                case "title-desc":
                    genres = genres.OrderByDescending(x => x.Value);    
                    break;
                default:
                    genres = genres.OrderBy(x => x.Value);
                    break;
            }

            return View(genres.ToPagedList(pageCurrent, pageMaxSize)); 
        }

        // GET: Genres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Value")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genre);
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Value")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Genre genre = db.Genres.Find(id);
            db.Genres.Remove(genre);
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
