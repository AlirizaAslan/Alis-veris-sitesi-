using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBMS.Controllers
{
    public class SubeController : Controller
    {
        DataEntities db = new DataEntities();
        public ActionResult Index()
        {
            var model = db.Sube.ToList();
            return View(model);
        }
        public ActionResult Arama(int tercih, string m)
        {
            if (m == "")
            {

                return RedirectToAction("Index", "Sube");
            }
            if (tercih == 1)
            {
                

                var model = db.Sube.Where(x => x.Sehir == m).ToList();

               

                return View("Index", model);
            }
            else if (tercih==2)
            {
                var model = db.Sube.Where(x => x.Cadde_Sokak == m).ToList();
               
                return View("Index", model);
            }
            else
            {
                int a;
                try
                {
                    a = int.Parse(m);
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Sube");

                }
                var model = db.Sube.Where(x => x.PostaKodu == a).ToList();

                

                return View("Index", model);
            }
            
        }
    }
}