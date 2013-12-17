using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProprioInfini.Controllers
{
    public class RapportController : Controller
    {
        //
        // GET: /Rapport/

        public ActionResult Index()
        {
            return View("~/Views/Rapports/Rapport1.cshtml");
        }

        //
        // GET: /Rapport/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Rapport/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Rapport/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Rapport/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Rapport/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Rapport/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Rapport/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public FileContentResult CreatePDF()
        {


            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            // Setup the report viewer object and get the array of bytes
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Rapports/AssReport.rdlc");

            /*ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "./Rapports/Proprietes.rdlc";*/

            e525_A13_P2_E1111FFFFFFDataSetTableAdapters.AnnonceTableAdapter ds = new e525_A13_P2_E1111FFFFFFDataSetTableAdapters.AnnonceTableAdapter();
            ReportDataSource rds = new ReportDataSource("DataSet1", (DataTable)ds.GetData());
            localReport.DataSources.Add(rds);

            byte[] bytes = localReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            return File(bytes, mimeType);

        }
        public FileContentResult CreatePDF3()
        {


            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            // Setup the report viewer object and get the array of bytes
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Rapports/AssReport3.rdlc");

            /*ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "./Rapports/Proprietes.rdlc";*/

            e525_A13_P2_E1111FFFFFFDataSet3TableAdapters.BatimentTableAdapter ds = new e525_A13_P2_E1111FFFFFFDataSet3TableAdapters.BatimentTableAdapter();
            ReportDataSource rds = new ReportDataSource("DataSet3", (DataTable)ds.GetData());
            localReport.DataSources.Add(rds);

            byte[] bytes = localReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            return File(bytes, mimeType);

        }
    }
}
