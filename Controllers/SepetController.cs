using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBMS.ViewModels;
namespace DBMS.Controllers
{
    [Authorize]
    public class SepetController : Controller
    {
        DataEntities db = new DataEntities();
        [Authorize]
        public ActionResult Index()
        {
            int kullaniciID = Int32.Parse((string)Session["kullaniciID"]);
            decimal tutar = 0;
            SepetViewModel sepetViewModel = new SepetViewModel
            {
                Sepettekiler = db.Sepet.Where(x => x.KullaniciID ==kullaniciID).ToList(),
                Adresler = db.Adres.Where(x => x.KullaniciID == kullaniciID).ToList(),
                KrediKartlari = db.KrediKarti.Where(x => x.KullaniciID == kullaniciID).ToList(),
                Emailler = db.Email.Where(x=>x.KullaniciID==kullaniciID).ToList()
            };
            foreach (var item in sepetViewModel.Sepettekiler)
            {
                tutar += item.Miktar * item.Urun.Fiyat;
            }
            ViewData["ToplamTutar"] = tutar;
            if (db.Musteri.Find(kullaniciID).Puan>=50)
            {
                ViewData["IndirimliToplamTutar"] = (tutar*95)/100;
            }
            else
            {
                ViewData["IndirimliToplamTutar"] = tutar;
            }
            return View(sepetViewModel);
        }
        [Route("Sepet/SepeteEkle/{BarkodNo}/{Renk}/{Beden}/{Miktar}")]
        public ActionResult SepeteEkle(int BarkodNo, string Renk, string Beden, int Miktar)
        {
            int kullaniciID = Int32.Parse((string)Session["kullaniciID"]);
            Sepet sepet = new Sepet
            {
                BarkodNo = BarkodNo,
                KullaniciID = kullaniciID,
                Miktar = Miktar,
                Renk = Renk,
                Beden = Beden
                

            };
            db.Sepet.Add(sepet);
            db.SaveChanges();
            return RedirectToAction("Index", "Urun", new { KategoriAd = db.Urun.Find(BarkodNo).Kategori,Cinsiyet= db.Urun.Find(BarkodNo).Cinsiyet});
        }
        [Route("Sepet/Sil/{sepetid}")]
        public ActionResult Sil(int sepetid )
        {
            var model = db.Sepet.Find(sepetid);
            db.Sepet.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
        [Route("Sepet/Artir/{sepetid}")]
        public ActionResult Artir(int sepetid)
        {
            var model = db.Sepet.Find(sepetid);
            model.Miktar = model.Miktar + 1;
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        [Route("Sepet/Azalt/{sepetid}")]
        public ActionResult Azalt(int sepetid)
        {
            var model = db.Sepet.Find(sepetid);
            if (model.Miktar==1)
            {
                return RedirectToAction("Index");
            }
            model.Miktar = model.Miktar - 1;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        

    }
}