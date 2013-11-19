using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProprioInfini.Controllers
{
    public class ProprietaireController : Controller
    {
        private ProprioInfiniConn db = new ProprioInfiniConn();

        //
        // GET: /Proprietaire/

        public ActionResult Index()
        {
            var proprietaires = db.Proprietaires.Include(p => p.Adresse);
            return View(proprietaires.ToList());
        }

        //
        // GET: /Proprietaire/Details/5

        public ActionResult Details(int id = 0)
        {
            Proprietaire proprietaire = db.Proprietaires.Find(id);
            if (proprietaire == null)
            {
                return HttpNotFound();
            }
            return View(proprietaire);
        }

        //
        // GET: /Proprietaire/Create

        public ActionResult Create()
        {
            ViewBag.AdresseId = new SelectList(db.Adresses, "Id", "CodePostal");
            return View();
        }

        //
        // POST: /Proprietaire/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Proprietaire proprietaire)
        {
            if (ModelState.IsValid)
            {
                db.Proprietaires.Add(proprietaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdresseId = new SelectList(db.Adresses, "Id", "CodePostal", proprietaire.AdresseId);
            return View(proprietaire);
        }

        //
        // GET: /Proprietaire/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Proprietaire proprietaire = db.Proprietaires.Find(id);
            if (proprietaire == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdresseId = new SelectList(db.Adresses, "Id", "CodePostal", proprietaire.AdresseId);
            return View(proprietaire);
        }

        //
        // POST: /Proprietaire/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Proprietaire proprietaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proprietaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdresseId = new SelectList(db.Adresses, "Id", "CodePostal", proprietaire.AdresseId);
            return View(proprietaire);
        }

        //
        // GET: /Proprietaire/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Proprietaire proprietaire = db.Proprietaires.Find(id);
            if (proprietaire == null)
            {
                return HttpNotFound();
            }
            return View(proprietaire);
        }

        //
        // POST: /Proprietaire/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proprietaire proprietaire = db.Proprietaires.Find(id);
            db.Proprietaires.Remove(proprietaire);
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