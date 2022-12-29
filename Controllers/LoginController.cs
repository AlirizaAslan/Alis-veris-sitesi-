using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DBMS.Controllers
{
    public class LoginController : Controller
    {
        DataEntities db = new DataEntities();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Sifre)
        {
            int kullaniciID=0;
           
            var model = db.Email.ToList();
            foreach (var item in model)
            {
                if (item.Email1 != Email)
                {
                    continue;
                }
                kullaniciID = item.KullaniciID;
                break;
            }
            if (kullaniciID == 0)
            {
                ViewBag.mesaj = "Geçersiz Email Girişi.Lütfen Tekrar Deneyiniz";
                return View();
            }
            string sifre = db.Kullanici.Find(kullaniciID).Sifre;

            if (sifre!=Sifre)
            {
                ViewBag.mesaj = "Geçersiz Şifre Girişi.Lütfen Tekrar Deneyiniz";
                return View();
            }
            string kullaniciAd = db.Kullanici.Find(kullaniciID).Ad;
            string kullaniciSoyad = db.Kullanici.Find(kullaniciID).Soyad;
            FormsAuthentication.SetAuthCookie(kullaniciAd+" "+kullaniciSoyad, true);
            Session["kullaniciID"] = kullaniciID.ToString();
            if (db.Kullanici.Find(kullaniciID).Yetki=="Y")
            {
                return RedirectToAction("Index", "Yonetici");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public ActionResult KayitOl()
        {

            return View();
        }
        [HttpPost]
        public ActionResult KayitOl(Kullanici kullanici,string Email)
        {
            kullanici.Yetki = "K";
            db.Kullanici.Add(kullanici);
            db.SaveChanges();
            var kullaniciID = db.Kullanici.Max(x => x.KullaniciID);
            Email email = new Email()
            {
                KullaniciID = kullaniciID,
                Email1 = Email

            };
            Musteri musteri = new Musteri()
            {
                KullaniciID = kullaniciID,
                Bakiye = 0,
                Puan = 0
            };
            db.Email.Add(email);
            db.Musteri.Add(musteri);
            db.SaveChanges();
            return RedirectToAction("Login","Login");
        }

    }
}