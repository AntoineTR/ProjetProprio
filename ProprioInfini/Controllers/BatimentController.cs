using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProprioInfini.Controllers
{
    public class BatimentController : Controller
    {
        private ProprioInfiniConn db = new ProprioInfiniConn();

        //
        // GET: /Batiment/

        public ActionResult Index()
        {
            var batiments = db.Batiments.Include(b => b.Adresse);
            return View(batiments.ToList());
        }

        //
        // GET: /Batiment/Details/5

        public ActionResult Details(int id = 0)
        {
            Batiment batiment = db.Batiments.Find(id);
            if (batiment == null)
            {
                return HttpNotFound();
            }
            return View(batiment);
        }

        //
        // GET: /Batiment/Create

        public ActionResult Create()
        {
            ViewBag.AdresseId = new SelectList(db.Adresses, "Id", "CodePostal");
            return View();
        }

        //
        // POST: /Batiment/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Batiment batiment)
        {
            if (ModelState.IsValid)
            {
                db.Batiments.Add(batiment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdresseId = new SelectList(db.Adresses, "Id", "CodePostal", batiment.AdresseId);
            return View(batiment);
        }

        //
        // GET: /Batiment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Batiment batiment = db.Batiments.Find(id);
            if (batiment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdresseId = new SelectList(db.Adresses, "Id", "CodePostal", batiment.AdresseId);
            return View(batiment);
        }

        //
        // POST: /Batiment/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Batiment batiment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(batiment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdresseId = new SelectList(db.Adresses, "Id", "CodePostal", batiment.AdresseId);
            return View(batiment);
        }

        //
        // GET: /Batiment/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Batiment batiment = db.Batiments.Find(id);
            if (batiment == null)
            {
                return HttpNotFound();
            }
            return View(batiment);
        }

        //
        // POST: /Batiment/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Batiment batiment = db.Batiments.Find(id);
            db.Batiments.Remove(batiment);
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