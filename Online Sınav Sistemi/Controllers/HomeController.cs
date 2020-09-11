using Online_Sınav_Sistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Online_Sınav_Sistemi.Controllers
{
    public class HomeController : Controller
    {

        private OnlineSınavEntities db = new OnlineSınavEntities();

        public ActionResult Index()
        {
            

            return View(db.Soru.ToList());
        }

        public ActionResult Mesajlar()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Sınavlar()
        {
            var uyeid = Session["UyeID"];
            int UyeİD = Convert.ToInt32(uyeid);
            //var dersYetki = db.DersYetki.Where(a => a.UyeID).Include(d => d.Uye);
            var res = db.DersYetki.Where(x => x.UyeID == UyeİD ).ToList();
            return View(res);
            //return View(db.Sınav.ToList());

        }

        //public ActionResult SaveExamAnswers(ExamViewModel model)
        //{
        //    var uyeid = Session["UyeID"];
        //    if (model == null)
        //        return HttpNotFound();


        //    //burda modelden Question larin cevaplari dongu ile db ye yazilacak
        //    var answers = new List<Cevap>();
        //    foreach (var question in model)
        //    {
        //        Cevap answer = new Cevap()
        //        {
        //             var cevap = new Cevap { CevapID = 2, UyeID = Convert.ToInt32(uyeid),
        //                 SoruID = question.questionId,  SecilenCevapID = question.SelectedChoiceId,
        //                 Puan = question.questionPoint, DogruCevapMı=question.RightChoiceId }
        //            //burda question dan cevap property lerini doldur 
        //        };
        //        answers.Add(answer);
        //    }

        //    db.Cevap.AddRange(answers);
        //    db.SaveChanges();

        //    return View();
        //}

        public ActionResult Sınav(int? id)
        {
            if (id == null)
                return HttpNotFound();

            ExamViewModel model = new ExamViewModel() { Id = id.Value };
            var sınav = db.Sınav.Where(m => m.SınavID == id).SingleOrDefault();
            var exam = db.Sınav.Where(m => m.SınavID == id).SingleOrDefault();
            if (exam != null)
            {
                model.Duration = exam.SınavSüresi.Value;
                model.Title = exam.SınavAdi;
                model.Questions = (from q in db.Soru.Where(a=>a.SınavID==id)
                                   select new QuestionViewModel()
                                   {
                                       Id = q.SoruID,
                                       ExamId = q.SınavID.Value,
                                       Title = q.SoruAdı,
                                       Choices = db.Secenek.Where(a => a.SoruID == q.SoruID).ToList()
                                   }).ToList();
                //model.Questions = db.Soru.Where(a => a.SınavID == model.Id).ToList();
            }
            else
                return HttpNotFound();

            if (sınav == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [System.Web.Services.WebMethod]
        public JsonResult secenekDegistir(int checked_option_radio, int SoruID)
        {
            var uyeid = Session["UyeID"];
            int uyeID = Convert.ToInt32(uyeid);
            System.Data.SqlClient.SqlConnection baglanti;
            baglanti = new System.Data.SqlClient.SqlConnection(@"server=.;database=OnlineSınav;Trusted_Connection=yes");
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("Select * from Cevap where SoruID=" + SoruID + " and UyeID=" + uyeID + "", baglanti);
            baglanti.Open();
            cmd.Connection = baglanti;
            int cevapVarmı = Convert.ToInt32(cmd.ExecuteScalar());
            if (cevapVarmı == 0)
            {
                if (checked_option_radio == null)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);

                }
                db.Cevap.Add(new Cevap { UyeID = Convert.ToInt32(uyeid), SoruID = SoruID, SecilenCevapID = checked_option_radio, Puan = 10 });
                db.SaveChanges();

            }
            else
            {
                baglanti = new System.Data.SqlClient.SqlConnection(@"server=.;database=OnlineSınav;Trusted_Connection=yes");
                cmd = new System.Data.SqlClient.SqlCommand("Update Cevap set SecilenCevapID='" + checked_option_radio + "' Where SoruID=" + SoruID + " and UyeID=" + uyeID + "", baglanti);// Sıkıntı var.
                baglanti.Open();
                cmd.ExecuteNonQuery();
                baglanti.Close();
            }
            baglanti.Close();
            return Json(false, JsonRequestBehavior.AllowGet);
        }




        [System.Web.Services.WebMethod]
        public JsonResult TikTarihKaydet(int butonID, int SoruID)
        {
            var uyeid = Session["UyeID"];
            int uyeID = Convert.ToInt32(uyeid);
            DateTime date = DateTime.Now;
            System.Data.SqlClient.SqlConnection baglanti;
            baglanti = new System.Data.SqlClient.SqlConnection(@"server=.;database=OnlineSınav;Trusted_Connection=yes");
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("Select * from DersYetki where DersYetkiID=" + butonID + "", baglanti);
            baglanti.Open();
            cmd.Connection = baglanti;
            int cevapVarmı = Convert.ToInt32(cmd.ExecuteScalar());
            if (cevapVarmı == 0)
            {
                if (butonID == null)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);

                }
                db.Cevap.Add(new Cevap { UyeID = Convert.ToInt32(uyeid), SoruID = SoruID, SecilenCevapID = butonID, Puan = 10 });
                db.SaveChanges();

            }
            else
            {
                baglanti = new System.Data.SqlClient.SqlConnection(@"server=.;database=OnlineSınav;Trusted_Connection=yes");
                cmd = new System.Data.SqlClient.SqlCommand("Update Cevap set SecilenCevapID='" + butonID + "' Where SoruID=" + SoruID + " and UyeID=" + uyeID + "", baglanti);// Sıkıntı var.
                baglanti.Open();
                cmd.ExecuteNonQuery();
                baglanti.Close();
            }
            baglanti.Close();
            return Json(false, JsonRequestBehavior.AllowGet);
        }



    }
}