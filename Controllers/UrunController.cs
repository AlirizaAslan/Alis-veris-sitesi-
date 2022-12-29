using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBMS.ViewModels;
namespace DBMS.Controllers
{
    public class UrunController : Controller
    {
        DataEntities db = new DataEntities();
        [Route("Urun/Index/{KategoriAd}/{Cinsiyet}")]
        public ActionResult Index(string KategoriAd,string Cinsiyet)
        {
            var model = db.Urun.Where(x => x.Kategori == KategoriAd && x.Cinsiyet==Cinsiyet).ToList();
            ViewBag.kategori = KategoriAd;
            ViewBag.cinsiyet = Cinsiyet;
            return View(model);
        }
        public ActionResult Arama(bool tercih,string m,string KategoriAd,string Cinsiyet)
        {
            if (m == "")
            {

                return RedirectToAction("Index","Urun", new {KategoriAd=KategoriAd,Cinsiyet=Cinsiyet } );
            }
            if (tercih==true)
            {
                int a;
                try
                {
                    a = int.Parse(m);
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Urun", new { KategoriAd = KategoriAd, Cinsiyet = Cinsiyet });

                }
               
                var model = db.Urun.Where(x => x.Kategori == KategoriAd && x.BarkodNo == a).ToList();
               
                if (model.Count==0)
                {
                    return RedirectToAction("Index", "Urun", new { KategoriAd = KategoriAd, Cinsiyet = Cinsiyet });
                }

                return View("Index", model);
            }
            else
            {
                var model = db.Urun.Where(x => x.Kategori == KategoriAd && x.Ad.Contains(m)).ToList();
                if (model.Count == 0)
                {
                    return RedirectToAction("Index", "Urun", new { KategoriAd = KategoriAd, Cinsiyet = Cinsiyet });
                }
                return View("Index", model);
            }
        }

        public ActionResult CokSatanlar()
        {
            var model = db.Urun.Where(x => x.Cinsiyet == "Kadın").OrderByDescending(x => x.SatisSayisi).Take(5).ToList();
            return View("Index", model);
        }
        public ActionResult YeniGelenler()
        {
            var model = db.Urun.Where(x => x.Cinsiyet == "Kadın").OrderByDescending(x => x.EklenmeTarihi).Take(5).ToList();
            return View("Index", model);
        }
        [Route("Urun/Cocuk/{Cinsiyet}/{id}")]
        public ActionResult Cocuk(string Cinsiyet,int id)
        {
            var model = db.Urun.Where(x => x.Cinsiyet == Cinsiyet && x.YasAraligi==id.ToString()).ToList();
            return View( model);
        }
        
        //public ActionResult YeniGelenler()
        //{
        //    var model = db.Urun.Where(x => x.Cinsiyet == "Kadın").OrderByDescending(x => x.EklenmeTarihi).Take(5).ToList();
        //    return View("Index", model);
        //}

        [Route("Urun/UrunGoruntule/{BarkodNo}")]
        public ActionResult UrunGoruntule(int BarkodNo)
        {
            var model = db.FisUrun.Where(x => x.BarkodNo == BarkodNo).ToList();
            
            UrunGoruntule urunGoruntule = new UrunGoruntule
            {
                Urun = db.Urun.Find(BarkodNo),
                Renkler = (from item in db.Renk where item.BarkodNo == BarkodNo select item).ToList(),
                Renk = (from item in db.Renk where item.BarkodNo == BarkodNo select item).ToList()[0],
                Bedenler = (from item in db.Beden where item.BarkodNo == BarkodNo select item).ToList(),
                Beden = (from item in db.Beden where item.BarkodNo == BarkodNo select item).ToList()[0],
                Miktar = 1,
                FisUrun=model
            };
            return View(urunGoruntule);
        }
        public ActionResult Sec(UrunIndex UrunIndex)
        {
            var model = db.FisUrun.Where(x => x.BarkodNo == UrunIndex.BarkodNo).ToList();
            UrunGoruntule urunGoruntule = new UrunGoruntule
            {
                Urun = db.Urun.Find(UrunIndex.BarkodNo),
                Renkler = (from item in db.Renk where item.BarkodNo ==UrunIndex.BarkodNo  select item).ToList(),
                Renk = (from item in db.Renk where item.Renk1 == UrunIndex.UrunRenk select item).ToList()[0],
                Bedenler = (from item in db.Beden where item.BarkodNo == UrunIndex.BarkodNo select item).ToList(),
                Beden = (from item in db.Beden where item.Beden1 == UrunIndex.UrunBeden select item).ToList()[0],
                Miktar = UrunIndex.Miktar,
                FisUrun=model

            };
            return View("UrunGoruntule", urunGoruntule);
        }
        
       

    }
}