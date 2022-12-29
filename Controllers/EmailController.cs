using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBMS.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        DataEntities db = new DataEntities();
        public ActionResult Index()
        {
            return View();
        }
        
        
        public ActionResult Sil(string email)
        { 
            var model = db.Email.Find(email);
            db.Email.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index", "Kullanici");

        }
        [HttpGet]
        public ActionResult Ekle()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(string email)
        {
            int kullaniciID = Int32.Parse((string)Session["kullaniciID"]);
            Email email1 = new Email()
            {
                Email1 = email,
                KullaniciID = kullaniciID

            };
            db.Email.Add(email1);
            db.SaveChanges();
            return RedirectToAction("Index", "Kullanici");
        }

    }
}