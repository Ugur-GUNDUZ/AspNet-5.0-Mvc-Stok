using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.entity;

namespace MvcStok.Controllers
{
    public class SatisController : Controller
    {

        // GET: Satis
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(TblSatislar p1)
        {
            db.TblSatislar.Add(p1);
            db.SaveChanges();
            return View("Index");
        }
    }
}