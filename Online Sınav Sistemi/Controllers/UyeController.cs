using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Online_Sınav_Sistemi.Models;

namespace Online_Sınav_Sistemi.Controllers
{
    public class UyeController : Controller
    {

        private OnlineSınavEntities db = new OnlineSınavEntities();
       

        // GET: Uye
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Uye uye)
        {
            var login = db.Uye.Where(u => u.Numara == uye.Numara).SingleOrDefault();
            if (login.Numara == uye.Numara && login.Sifre == uye.Sifre)
            {
                Session["UyeID"] = login.UyeID;
                Session["Numara"] = login.Numara;
                Session["YetkiID"] = login.YetkiID;
                Session["YetkiAdi"] = login.Ad;
                if (login.YetkiID == 1) { return Redirect("~/Admin/AdminHome/Index"); }
                else if (login.YetkiID == 2) { return Redirect("~/Uye/Index"); /*RedirectToAction("Index", "Uye");*/ }               
                else return Redirect("~/Home/Index"); /*RedirectToAction("Index", "Home");*/

            }
            else
            {
                ViewBag.Uyarı = "Numara ya da Şifrenizi kontrol ediniz!";
                return View();
            }

        }

        
        [HttpPost]
        public ActionResult YeniUyelik(Uye model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
 
            Uye uye = new Uye();
            foreach (var item in db.Uye)
            {
                if (item.Email == model.Email)
                {
                    ViewBag.Uyarı = "Böyle bir E-Posta mevcut lütfen farklı bir E-Posta giriniz.";
                    return View();
                }
                else uye.Email = model.Email;
                uye.Sifre = model.Sifre;
                uye.Ad = model.Ad;
                uye.Soyad = model.Soyad;
                if (item.Numara == model.Numara)
                {
                    ViewBag.Uyarı = "Böyle bir Numara mevcut lütfen farklı bir Numara giriniz.";
                    return View();
                }
                else uye.Numara = model.Numara;
            }

            uye.YetkiID = 3;
                db.Uye.Add(uye);
                db.SaveChanges();

                //İşlemimiz başarıyla biterse, başarılı olduğuna dair bir sayfaya yönlendiriyoruz.
                return Redirect("~/Uye/Login");
            
        }

        public ActionResult Logout()
        {
            Session["UyeID"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Uye");
        }
        
    }
}