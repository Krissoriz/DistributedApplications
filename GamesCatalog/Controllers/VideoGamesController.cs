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
    public class VideoGamesController : Controller
    {
        private VideoGameCatalogDBContext db = new VideoGameCatalogDBContext();

        // GET: VideoGames
        public ActionResult Index(string searchTitle, int? searchGenreId, int? page, string sortOrder)
        {
            int pageCurrent = page ?? 1;
            int pageMaxSize = 8;

            var videoGames = db.VideoGames.Include(m => m.Genre).AsQueryable();
            ViewBag.Genres = new SelectList(db.Genres, "Id", "Value");
            ViewBag.TitleSearch = searchTitle;

            if (!string.IsNullOrEmpty(searchTitle))
                videoGames = videoGames.Where(x => x.Title.Contains(searchTitle));

            if (searchGenreId.HasValue)
                videoGames = videoGames.Where(x => x.GenreId == searchGenreId);

            ViewBag.SortOrder = sortOrder;
            ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title-desc" : "";
            ViewBag.ReleaseDateSortParam = sortOrder == "rdate-desc" ? "rdate-asc" : "rdate-desc";

            switch (sortOrder)
            {
                case "title-desc":
                    videoGames = videoGames.OrderByDescending(x => x.Title);
                    break;
                case "rdate-asc":
                    videoGames = videoGames.OrderBy(x => x.ReleaseDate);
                    break;
                case "rdate-desc":
                    videoGames = videoGames.OrderByDescending(x => x.ReleaseDate);
                    break;
                default:
                    videoGames = videoGames.OrderBy(x => x.Title);
                    break;
            }

            return View(videoGames.ToPagedList(pageCurrent, pageMaxSize));
        }

        // GET: VideoGames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoGame videoGame = db.VideoGames.Find(id);
            if (videoGame == null)
            {
                return HttpNotFound();
            }
            return View(videoGame);
        }

        // GET: VideoGames/Create
        public ActionResult Create()
        {
            ViewBag.DevelopersId = new SelectList(db.Developers, "Id", "Name");
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Value");
            return View();
        }

        // POST: VideoGames/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ReleaseDate,UserRating,GenreId,DevelopersId")] VideoGame videoGame)
        {
            if (ModelState.IsValid)
            {
                db.VideoGames.Add(videoGame);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DevelopersId = new SelectList(db.Developers, "Id", "Name", videoGame.DevelopersId);
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Value", videoGame.GenreId);
            return View(videoGame);
        }

        // GET: VideoGames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoGame videoGame = db.VideoGames.Find(id);
            if (videoGame == null)
            {
                return HttpNotFound();
            }
            ViewBag.DevelopersId = new SelectList(db.Developers, "Id", "Name", videoGame.DevelopersId);
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Value", videoGame.GenreId);
            return View(videoGame);
        }

        // POST: VideoGames/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ReleaseDate,UserRating,GenreId,DevelopersId")] VideoGame videoGame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videoGame).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DevelopersId = new SelectList(db.Developers, "Id", "Name", videoGame.DevelopersId);
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Value", videoGame.GenreId);
            return View(videoGame);
        }

        // GET: VideoGames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoGame videoGame = db.VideoGames.Find(id);
            if (videoGame == null)
            {
                return HttpNotFound();
            }
            return View(videoGame);
        }

        // POST: VideoGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VideoGame videoGame = db.VideoGames.Find(id);
            db.VideoGames.Remove(videoGame);
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
