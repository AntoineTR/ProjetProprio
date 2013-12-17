﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Spatial;
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
            List<string> lBatisse = new List<string>();

            for (int i = 1; i < 21; i++)
            {
                lBatisse.Add(i + "");
            }            
            ViewBag.listBatisse = db.Annonces.ToList();
            ViewBag.listRegion = new SelectList(db.Regions, "Id", "Nom");
            
            ViewBag.listNbPiece = new SelectList(lBatisse,"1"); 
            
            return View(annonces.ToList());
        }
        


        public List<DbGeography> getBounds(string region)
        {
            List<DbGeography> allPointForOneRegion = new List<DbGeography>();
            foreach (Region item in db.Regions.ToList())
            {       
                if (region == item.nom)
                {
                    for (int i = 1; i < item.geometrie.PointCount; i += 10)
                    {
                        allPointForOneRegion.Add(item.geometrie.PointAt(i));
                    }
                }
            }
            return allPointForOneRegion;
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
            string numCivic = adresse.Substring(0, adresse.IndexOf(' '));
            List<Annonce> lAnnonces = db.Annonces.ToList().FindAll(x => x.Batiment.Adresse.NumeroCivic == int.Parse(numCivic));

            string contenu = "<table><tr><th>Nom du Proprio</th><th>Date Debut Annonce</th><th>Date Fin Annonce</th><th>Prix</th><th>Batiment</th><th>Titre de LANNONCE</th>";
            foreach (Annonce item in lAnnonces)
            {
                contenu += "<tr><td>" + item.Proprietaire.Nom + "</td><td>" + item.DateDebutAnnonce.Value.ToShortDateString() + "</td><td>" + item.DateFinAnnonce.Value.ToShortDateString() + "</td><td>" + item.Prix.ToString() + "</td><td>" + item.Batiment.Nom + "</td><td>" + item.Titre + "</td></tr>";
            }
            return Content(contenu);
        }

        [HttpPost]
        public ActionResult ZoomMAPPartial(string region)
        {
            
            List<DbGeography> yo = getBounds(region);
            string  ye = "";
            foreach (DbGeography item in yo)
            {
                ye += item.Latitude.ToString() + "$" + item.Longitude.ToString() + "/";
            }
           return Content(ye);
        }

        [HttpPost]
        public ActionResult AjaxFilter(string nombrePiece, string prixDebut, string prixFin, string region)
        {

            List<Annonce> allAnnonces = new List<Annonce>();
            if (nombrePiece == "Tout les nombres" && region == "Aucune ville")
                allAnnonces = (List<Annonce>)db.Annonces.ToList().FindAll(a => a.Prix >= int.Parse(prixDebut) && a.Prix <= int.Parse(prixFin));
            else if (region == "Aucune ville")
                allAnnonces = (List<Annonce>)db.Annonces.ToList().FindAll(a => a.Prix >= int.Parse(prixDebut) && a.Prix <= int.Parse(prixFin) && a.Batiment.NombrePiece == int.Parse(nombrePiece));
            else if (nombrePiece == "Tout les nombres")
                allAnnonces = (List<Annonce>)db.Annonces.ToList().FindAll(a => a.Prix >= int.Parse(prixDebut) && a.Prix <= int.Parse(prixFin) && a.Batiment.Adresse.Ville.Nom == region);
            else
                allAnnonces = (List<Annonce>)db.Annonces.ToList().FindAll(a => a.Batiment.NombrePiece == int.Parse(nombrePiece) && a.Prix >= int.Parse(prixDebut) && a.Prix <= int.Parse(prixFin) && a.Batiment.Adresse.Ville.Nom == region);

            string divToReturn = "";

            foreach (Annonce a in allAnnonces)
            {
                //ca marche a peu pres
                divToReturn += "<div class=\"geoloco\" id=\""+ a.Batiment.Adresse.NumeroCivic +" " + a.Batiment.Adresse.Rue.Nom +" " + a.Batiment.Adresse.Ville.Nom + "\"></div>";
            }

           return Content(divToReturn);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}