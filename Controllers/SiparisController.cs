using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBMS.Controllers
{
    [Authorize]
    public class SiparisController : Controller
    {
        DataEntities db = new DataEntities();
        [Authorize]
        public ActionResult Index()
        {
            int kullaniciID = Int32.Parse((string)Session["kullaniciID"]);
            var model = db.Fis.Where(x => x.KullaniciID == kullaniciID).ToList();
            int kadin = 0;int erkek = 0;int cocuk = 0;int guzellik = 0;


            foreach (var item in model)
            {
                foreach (var item2 in item.FisUrun)
                {
                    if (item2.Urun.Cinsiyet=="Kadın")
                    {
                        kadin += 1;
                    }
                    else if (item2.Urun.Cinsiyet == "Erkek")
                    {
                        erkek += 1;
                    }
                     else if (item2.Urun.Cinsiyet == "Kız Çocuk" || item2.Urun.Cinsiyet == "Erkek Çocuk")
                    {
                        cocuk += 1;
                    }
                    else
                    {
                        guzellik += 1;

                    }
                }
            }
            if ((erkek + kadin + cocuk + guzellik)==0)
            {
                ViewBag.kadin = 0.ToString() + "%";
                ViewBag.erkek = 0.ToString() + "%";
                ViewBag.guzellik = 0.ToString() + "%";
                ViewBag.cocuk =0.ToString() + "%";
            }
            else
            {
                ViewBag.kadin = (kadin * 100 / (erkek + kadin + cocuk + guzellik)).ToString() + "%";
                ViewBag.erkek = (erkek * 100 / (erkek + kadin + cocuk + guzellik)).ToString() + "%";
                ViewBag.guzellik = (guzellik * 100 / (erkek + kadin + cocuk + guzellik)).ToString() + "%";
                ViewBag.cocuk = (cocuk * 100 / (erkek + kadin + cocuk + guzellik)).ToString() + "%";
            }
            
            return View(model);
        }
        [HttpPost]
        public ActionResult SiparisVer(Fis fis, int id)
        {
            var Urunler = db.Sepet.ToList();
            if (Urunler.Count==0)
            {
                return RedirectToAction("Index", "Sepet");
            }
            int kullaniciID = Int32.Parse((string)Session["kullaniciID"]);
            var model = db.Adres.Find(id);
            fis.Sehir = model.Sehir;
            fis.Ilce = model.Ilce;
            fis.Cadde_Sokak = model.Cadde_Sokak;
            fis.Numara = model.Numara;
            fis.KullaniciID = kullaniciID;
            fis.Tarih = System.DateTime.Now;
            db.Fis.Add(fis);
            db.SaveChanges();

            
            foreach (var item in Urunler)
            {
                FisUrun fisUrun = new FisUrun()
                {
                    BarkodNo = item.BarkodNo,
                    FisID = db.Fis.Max(x => x.FisID),
                    IadeDurumu = false,
                    Miktar = item.Miktar

                };
                var urun = db.Urun.Find(item.BarkodNo);
                urun.SatisSayisi += ((short)item.Miktar);
                db.FisUrun.Add(fisUrun);
            }
            var sepets = db.Sepet.Where(x => x.KullaniciID == kullaniciID).ToList();
            foreach (var item in sepets)
            {
                db.Sepet.Remove(item);
            }

            db.SaveChanges();

            return RedirectToAction("Index","Siparis");
        }
        [Route("Siparis/UrunListele/{fisID}")]
        public ActionResult UrunListele(int fisID)
        {
            var model = db.FisUrun.Where(x => x.FisID == fisID).ToList();
            return View(model);
        }
        public ActionResult IadeEt(int id) 
        {
            var model = db.FisUrun.Find(id);
            model.IadeDurumu = true;
            var model2 = db.Urun.Find(model.BarkodNo);
            model2.IadeMiktari += model.Miktar;
            int fisid = db.FisUrun.Find(id).FisID;
            db.SaveChanges();
            return RedirectToAction("UrunListele", "Siparis", new { fisID = fisid });
        }
        [HttpGet]
        public ActionResult YorumYap(int id)
        {
            
            return View(id);
        }
        [HttpPost]
        public ActionResult YorumYap(string yorum,int id)
        {
            int kullaniciID = Int32.Parse((string)Session["kullaniciID"]);
            var musteri = db.Musteri.Find(kullaniciID);
            musteri.Puan += 5;
            var model = db.FisUrun.Find(id);
            model.Yorum = yorum;
            int fisid = model.FisID;
            db.SaveChanges();
            return RedirectToAction("UrunListele", "Siparis", new { fisID = fisid });
        }
        [HttpGet]
        public ActionResult YorumGuncelle(int id)
        {
            var model = db.FisUrun.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult YorumGuncelle(string yorum, int id)
        {
            var model = db.FisUrun.Find(id);
            model.Yorum = yorum;
            int fisid = model.FisID;
            db.SaveChanges();
            return RedirectToAction("UrunListele", "Siparis", new { fisID = fisid });
        }

    }
}