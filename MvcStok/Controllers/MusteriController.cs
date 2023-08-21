using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.entity;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            var arama = from d in db.TblMusteriler select d;
            if (!string.IsNullOrEmpty(p))
            {
                arama = arama.Where(m => m.MusteriAd.Contains(p));
            }
            //var musterilerlist = db.TblMusteriler.ToList();
            return View(arama.ToList());
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(TblMusteriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TblMusteriler.Add(p1); // p1 vtye kayıt et insert into özelliği
            db.SaveChanges(); // değişiklikleri kaydet
            return View();
        }
        public ActionResult Sil(int id)
        {
            var musteri = db.TblMusteriler.Find(id);
            db.TblMusteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Guncelle(int id)
        {
            var musteri = db.TblMusteriler.Find(id);
            return View("Guncelle", musteri);
        }
        public ActionResult MusteriGuncelle(TblMusteriler p1)
        {
            var musteri = db.TblMusteriler.Find(p1.MusteriId);
            musteri.MusteriAd = p1.MusteriAd;
            musteri.MusteriSoyad = p1.MusteriSoyad;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}