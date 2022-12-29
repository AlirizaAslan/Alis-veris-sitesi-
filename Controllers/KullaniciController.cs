using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBMS.ViewModels;
namespace DBMS.Controllers
{
    [Authorize]
    public class KullaniciController : Controller
    {
       
        DataEntities db = new DataEntities();
        
        public ActionResult Index()
        {
            int kullaniciID = Int32.Parse((string)Session["kullaniciID"]);
            KullaniciViewModel kullaniciViewModel = new KullaniciViewModel
            {
                Kullanici = db.Kullanici.Find(kullaniciID),
                Adresler = db.Adres.Where(x=>x.KullaniciID ==kullaniciID).ToList(),
                Emailler = db.Email.Where(x => x.KullaniciID == kullaniciID).ToList(),
                KrediKartlari = db.KrediKarti.Where(x => x.KullaniciID == kullaniciID).ToList()

            };
            return View(kullaniciViewModel);
        }
       
        
        [HttpGet]
        [Route("Kullanici/GuncelleGet/{kullaniciID}")]
        public ActionResult GuncelleGet(int kullaniciID)
        {
            var model = db.Kullanici.Find(kullaniciID);
            return View(model);
        }

        [HttpPost]
        
        public ActionResult GuncellePost(Kullanici kullanici)
        {
            var model = db.Kullanici.Find(kullanici.KullaniciID);
            model.Ad = kullanici.Ad;
            model.Soyad = kullanici.Soyad;
            model.Sifre = kullanici.Sifre;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Cuzdan()
        {
            int kullaniciID = Int32.Parse((string)Session["kullaniciID"]);
            CuzdanViewModel cuzdanViewModel = new CuzdanViewModel()
            {

                Musteri = db.Musteri.Find(kullaniciID),
                Kullanici=db.Kullanici.Find(kullaniciID),
                KrediKartlari = db.KrediKarti.Where(x => x.KullaniciID == kullaniciID).ToList()

            };
            return View(cuzdanViewModel);
        }
        [HttpGet]
        public ActionResult KrediKartiEkle()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult KrediKartiEkle(decimal KrediKartiNo)
        {
            int kullaniciID = Int32.Parse((string)Session["kullaniciID"]);
            KrediKarti krediKarti = new KrediKarti()
            {
                KrediKarti1 = KrediKartiNo,
                KullaniciID = kullaniciID
            };
            db.KrediKarti.Add(krediKarti);
            db.SaveChanges();
            return RedirectToAction("Cuzdan", "Kullanici");
        }
        public ActionResult KrediKartiSil(decimal KrediKartiNo)
        {
            var model = db.KrediKarti.Find(KrediKartiNo);
            db.KrediKarti.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Cuzdan", "Kullanici");
        }
        public ActionResult HesabimiSil(int id)
        {
            var model = db.Kullanici.Find(id);
            db.Kullanici.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Logout", "Login");
        }
    }
}