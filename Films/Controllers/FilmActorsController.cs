using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Films.Models;

namespace Films.Controllers
{
    public class FilmActorsController : Controller
    {
        private FilmsEntities db = new FilmsEntities();

        // GET: FilmActors
        [Authorize]
        public ActionResult Index()
        {
            var filmActors = db.FilmActors.Include(f => f.Actor).Include(f => f.Film).OrderBy(t=>t.Film.Title).ToList();
            return View(filmActors);
        }

        // GET: FilmActors/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilmActor filmActor = db.FilmActors.Find(id);
            if (filmActor == null)
            {
                return HttpNotFound();
            }
            return View(filmActor);
        }

        // GET: FilmActors/Create
        [Authorize (Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.ActorId = new SelectList(db.Actors, "Id", "Firstname", "Lastname");
            ViewBag.FilmId = new SelectList(db.Films, "Id", "Title");
            return View();
        }

        // POST: FilmActors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FilmId,ActorId")] FilmActor filmActor)
        {
            if (ModelState.IsValid)
            {
                db.FilmActors.Add(filmActor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActorId = new SelectList(db.Actors, "Id", "Firstname", "Lastname", filmActor.ActorId);
            ViewBag.FilmId = new SelectList(db.Films, "Id", "Title", filmActor.FilmId);
            return View(filmActor);
        }

        // GET: FilmActors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilmActor filmActor = db.FilmActors.Find(id);
            if (filmActor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActorId = new SelectList(db.Actors, "Id", "Firstname", "Lastname", filmActor.ActorId);
            ViewBag.FilmId = new SelectList(db.Films, "Id", "Title", filmActor.FilmId);
            return View(filmActor);
        }

        // POST: FilmActors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FilmId,ActorId")] FilmActor filmActor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filmActor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActorId = new SelectList(db.Actors, "Id", "Firstname", "Lastname", filmActor.ActorId);
            ViewBag.FilmId = new SelectList(db.Films, "Id", "Title", filmActor.FilmId);
            return View(filmActor);
        }

        // GET: FilmActors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilmActor filmActor = db.FilmActors.Find(id);
            if (filmActor == null)
            {
                return HttpNotFound();
            }
            return View(filmActor);
        }

        // POST: FilmActors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FilmActor filmActor = db.FilmActors.Find(id);
            db.FilmActors.Remove(filmActor);
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
