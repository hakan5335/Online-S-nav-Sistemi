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
    public class AdminSoruController : Controller
    {
        private OnlineSınavEntities db = new OnlineSınavEntities();

        // GET: /Soru/
        public ActionResult Index()
        {
            var soru = db.Soru.Include(s => s.Konu).Include(s => s.Sınav);
            return View(soru.ToList());
        }

        // GET: /Soru/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soru soru = db.Soru.Find(id);
            if (soru == null)
            {
                return HttpNotFound();
            }
            return View(soru);
        }

        // GET: /Soru/Create
        public ActionResult Create()
        {
            ViewBag.KonuID = new SelectList(db.Konu, "KonuID", "KonuAdi");
            ViewBag.SınavID = new SelectList(db.Sınav, "SınavID", "SınavAdi");
            return View();
        }

        // POST: /Soru/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SoruID,SınavID,KonuID,Puan,SoruResmi,SoruAdı,DogruCevapID")] Soru soru)
        {
            if (ModelState.IsValid)
            {
                db.Soru.Add(soru);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KonuID = new SelectList(db.Konu, "KonuID", "KonuAdi", soru.KonuID);
            ViewBag.SınavID = new SelectList(db.Sınav, "SınavID", "SınavAdi", soru.SınavID);
            return View(soru);
        }

        // GET: /Soru/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soru soru = db.Soru.Find(id);
            if (soru == null)
            {
                return HttpNotFound();
            }
            ViewBag.KonuID = new SelectList(db.Konu, "KonuID", "KonuAdi", soru.KonuID);
            ViewBag.SınavID = new SelectList(db.Sınav, "SınavID", "SınavAdi", soru.SınavID);
            return View(soru);
        }

        // POST: /Soru/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="SoruID,SınavID,KonuID,Puan,SoruResmi,SoruAdı,DogruCevapID")] Soru soru)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soru).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KonuID = new SelectList(db.Konu, "KonuID", "KonuAdi", soru.KonuID);
            ViewBag.SınavID = new SelectList(db.Sınav, "SınavID", "SınavAdi", soru.SınavID);
            return View(soru);
        }

        // GET: /Soru/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soru soru = db.Soru.Find(id);
            if (soru == null)
            {
                return HttpNotFound();
            }
            return View(soru);
        }

        // POST: /Soru/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Soru soru = db.Soru.Find(id);
            db.Soru.Remove(soru);
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
