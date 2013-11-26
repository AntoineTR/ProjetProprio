using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProprioInfini.Controllers
{
    public class AnnonceController : Controller
    {
        private ProprioInfiniConn db = new ProprioInfiniConn();

        //
        // GET: /Annonce/

        public ActionResult Index()
        {
            var annonces = db.Annonces.Include(a => a.Batiment).Include(a => a.Proprietaire);
            ViewBag.listBatisse = db.Annonces.ToList();
            ViewBag.listRegion = new SelectList(db.Regions, "Id", "Nom");
            return View(annonces.ToList());
        }

        //
        // GET: /Annonce/Details/5

        public ActionResult Details(int id = 0)
        {
            Annonce annonce = db.Annonces.Find(id);
            if (annonce == null)
            {
                return HttpNotFound();
            }
            return View(annonce);
        }

        //
        // GET: /Annonce/Create

        public ActionResult Create()
        {
            ViewBag.BatimentId = new SelectList(db.Batiments, "Id", "Nom");
            ViewBag.ProprietaireId = new SelectList(db.Proprietaires, "Id", "Nom");
            return View();
        }

        //
        // POST: /Annonce/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Annonce annonce)
        {
            if (ModelState.IsValid)
            {
                db.Annonces.Add(annonce);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BatimentId = new SelectList(db.Batiments, "Id", "Nom", annonce.BatimentId);
            ViewBag.ProprietaireId = new SelectList(db.Proprietaires, "Id", "Nom", annonce.ProprietaireId);
            return View(annonce);
        }

        //
        // GET: /Annonce/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Annonce annonce = db.Annonces.Find(id);
            if (annonce == null)
            {
                return HttpNotFound();
            }
            ViewBag.BatimentId = new SelectList(db.Batiments, "Id", "Id", annonce.BatimentId);
            ViewBag.ProprietaireId = new SelectList(db.Proprietaires, "Id", "Courriel", annonce.ProprietaireId);
            return View(annonce);
        }

        //
        // POST: /Annonce/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Annonce annonce)
        {
            if (ModelState.IsValid)
            {
                db.Entry(annonce).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BatimentId = new SelectList(db.Batiments, "Id", "Id", annonce.BatimentId);
            ViewBag.ProprietaireId = new SelectList(db.Proprietaires, "Id", "Courriel", annonce.ProprietaireId);
            return View(annonce);
        }

        //
        // GET: /Annonce/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Annonce annonce = db.Annonces.Find(id);
            if (annonce == null)
            {
                return HttpNotFound();
            }
            return View(annonce);
        }

        //
        // POST: /Annonce/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Annonce annonce = db.Annonces.Find(id);
            db.Annonces.Remove(annonce);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public ActionResult AdresseMAPPartial(string adresse)
        {
            string numCivic =  adresse.Substring(0,adresse.IndexOf(' '));
            List<Annonce> lAnnonces = db.Annonces.ToList().FindAll(x => x.Batiment.Adresse.NumeroCivic == int.Parse(numCivic));
            
            string contenu = "<table><tr><th>Nom du Proprio</th><th>Date Debut Annonce</th><th>Date Fin Annonce</th><th>Prix</th><th>Batiment</th><th>Titre de LANNONCE</th>";
            foreach (Annonce item in lAnnonces)
            {
                contenu += "<tr><td>" + item.Proprietaire.Nom + "</td><td>" + item.DateDebutAnnonce.Value.ToShortDateString() + "</td><td>" + item.DateFinAnnonce.Value.ToShortDateString() + "</td><td>" + item.Prix.ToString() + "</td><td>" + item.Batiment.Nom + "</td><td>" + item.Titre + "</td></tr>";
            }                   
            return Content(contenu);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}