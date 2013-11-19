using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProprioInfini.Controllers
{
    public class AdresseController : Controller
    {
        private ProprioInfiniConn db = new ProprioInfiniConn();

        //
        // GET: /Adresse/

        public ActionResult Index()
        {
            var adresses = db.Adresses.Include(a => a.Rue).Include(a => a.Ville);
            return View(adresses.ToList());
        }

        //
        // GET: /Adresse/Details/5

        public ActionResult Details(int id = 0)
        {
            Adresse adresse = db.Adresses.Find(id);
            if (adresse == null)
            {
                return HttpNotFound();
            }
            return View(adresse);
        }

        //
        // GET: /Adresse/Create

        public ActionResult Create()
        {
            ViewBag.RueId = new SelectList(db.Rues, "Id", "Nom");
            ViewBag.VilleId = new SelectList(db.Villes, "Id", "Nom");
            return View();
        }

        //
        // POST: /Adresse/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Adresse adresse)
        {
            if (ModelState.IsValid)
            {
                db.Adresses.Add(adresse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RueId = new SelectList(db.Rues, "Id", "Nom", adresse.RueId);
            ViewBag.VilleId = new SelectList(db.Villes, "Id", "Nom", adresse.VilleId);
            return View(adresse);
        }

        //
        // GET: /Adresse/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Adresse adresse = db.Adresses.Find(id);
            if (adresse == null)
            {
                return HttpNotFound();
            }
            ViewBag.RueId = new SelectList(db.Rues, "Id", "Nom", adresse.RueId);
            ViewBag.VilleId = new SelectList(db.Villes, "Id", "Nom", adresse.VilleId);
            return View(adresse);
        }

        //
        // POST: /Adresse/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Adresse adresse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adresse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RueId = new SelectList(db.Rues, "Id", "Nom", adresse.RueId);
            ViewBag.VilleId = new SelectList(db.Villes, "Id", "Nom", adresse.VilleId);
            return View(adresse);
        }

        //
        // GET: /Adresse/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Adresse adresse = db.Adresses.Find(id);
            if (adresse == null)
            {
                return HttpNotFound();
            }
            return View(adresse);
        }

        //
        // POST: /Adresse/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adresse adresse = db.Adresses.Find(id);
            db.Adresses.Remove(adresse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}