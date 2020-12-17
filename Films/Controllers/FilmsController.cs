using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Films.Models;
using PagedList;

namespace Films.Controllers
{
    public class FilmsController : Controller
    {
        private FilmsEntities db = new FilmsEntities();

        // GET: Films
        public ActionResult Films(int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            var films = db.Films.OrderBy(c => c.Country).ToList();
            return View(films.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult MyFilms()
        {
            var films = db.Films.OrderBy(t => t.Title).ToList();
            return View(films);
        }

        public ActionResult FilmSearch(string Country)
        {
            var films = db.Films.Where(t => t.Country.Contains(Country)).OrderBy(n => n.Title).ToList();
            if (films.Count <= 0)
            {
                return HttpNotFound();
            }
            return View(films);
        }

        // GET: Films/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }
        public FileContentResult GetImage(int id)
        {
            Actor actor = db.Actors.FirstOrDefault(g => g.Id == id);
            if (actor != null)
            {
                return File(actor.Photo, actor.PhotoType);
            }
            return null;
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
