using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBMS.Controllers
{
    public class YoneticiController : Controller
    {
        DataEntities db = new DataEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IadeEdilenler()
        {
            var model = db.Urun.OrderByDescending(x => x.IadeMiktari).Take(10).ToList();
            return View(model);
        }
        public ActionResult CokSatanlar()
        {
            var model = db.Urun.OrderByDescending(x => x.SatisSayisi).Take(10).ToList();
            return View(model);
        }
        public ActionResult Favori()
        {
            var model = db.Urun.OrderByDescending(x => x.FavoriSayisi).Take(10).ToList();
            return View(model);
        }
        public ActionResult EnCokPuanToplayanlar()
        {
            var model = db.Musteri.OrderByDescending(x=>x.Puan).Take(10).ToList();
            return View(model);
        }
        public ActionResult Yorum()
        {
            var model = db.FisUrun.ToList();
            return View(model);
        }
        public ActionResult YorumSil(int id)
        {
            var model = db.FisUrun.Find(id).Yorum = null;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}