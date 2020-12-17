using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Films.Models;

namespace Films.Controllers
{
    public class HomeController : Controller
    {
        private FilmsEntities db = new FilmsEntities();
        public ActionResult Index()
        {
            var allFilms = db.Films.OrderByDescending(v => v.Id).Take(3).ToList();
            return View(allFilms);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
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