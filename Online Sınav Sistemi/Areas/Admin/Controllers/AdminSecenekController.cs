using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Online_Sınav_Sistemi.Models;

namespace Online_Sınav_Sistemi.Areas.Admin.Controllers
{
    public class AdminSecenekController : Controller
    {
        private OnlineSınavEntities db = new OnlineSınavEntities();

        // GET: /Secenek/
        public ActionResult Index()
        {
            var secenek = db.Secenek.Include(s => s.Soru);
            return View(secenek.ToList());
        }

        // GET: /Secenek/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secenek secenek = db.Secenek.Find(id);
            if (secenek == null)
            {
                return HttpNotFound();
            }
            return PartialView(secenek);
        }

        // GET: /Secenek/Create
        public ActionResult Create()
        {
            ViewBag.SoruID = new SelectList(db.Soru, "SoruID", "SoruAdı");
            return View();
        }

        // POST: /Secenek/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SecenekID,SoruID,SecenekAdı,SecenekResmi")] Secenek secenek)
        {
            if (ModelState.IsValid)
            {
                db.Secenek.Add(secenek);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SoruID = new SelectList(db.Soru, "SoruID", "SoruAdı", secenek.SoruID);
            return View(secenek);
        }

        // GET: /Secenek/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secenek secenek = db.Secenek.Find(id);
            if (secenek == null)
            {
                return HttpNotFound();
            }
            ViewBag.SoruID = new SelectList(db.Soru, "SoruID", "SoruAdı", secenek.SoruID);
            return PartialView(secenek);
        }

        // POST: /Secenek/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="SecenekID,SoruID,SecenekAdı,SecenekResmi")] Secenek secenek)
        {
            if (ModelState.IsValid)
            {
                db.Entry(secenek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SoruID = new SelectList(db.Soru, "SoruID", "SoruAdı", secenek.SoruID);
            return PartialView(secenek);
        }

        // GET: /Secenek/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secenek secenek = db.Secenek.Find(id);
            if (secenek == null)
            {
                return HttpNotFound();
            }
            return PartialView(secenek);
        }

        // POST: /Secenek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Secenek secenek = db.Secenek.Find(id);
            db.Secenek.Remove(secenek);
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
