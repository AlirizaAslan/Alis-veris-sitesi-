using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBMS.Controllers
{
    [Authorize]
    public class AdresController : Controller
    {
        DataEntities db = new DataEntities();
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Adres adres)
        {
            int kullaniciID = Int32.Parse((string)Session["kullaniciID"]);
            adres.KullaniciID = kullaniciID;
            db.Adres.Add(adres);
            db.SaveChanges();
            return RedirectToAction("Index","Kullanici");
        }
        [HttpGet]
        [Route("Adres/Guncelle/{adresID}")]
        public ActionResult GuncelleGet(int adresID)
        {
            var model = db.Adres.Find(adresID);
            return View(model);
        }
        [HttpPost]
        public ActionResult GuncellePost(Adres adres)
        {
            var model = db.Adres.Find(adres.AdresID);
            model.Sehir = adres.Sehir;
            model.Ilce = adres.Ilce;
            model.Cadde_Sokak = adres.Cadde_Sokak;
            model.Numara = adres.Numara;
            db.SaveChanges();
            return RedirectToAction("Index", "Kullanici");
        }
        [Route("Adres/Sil/{adresID}")]
        public ActionResult Sil(int adresID)
        {
            var model = db.Adres.Find(adresID);
            db.Adres.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index", "Kullanici");
        }
    }
}