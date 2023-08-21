using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var urunlerlist = db.TblUrunler.ToList();
            return View(urunlerlist);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriId.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;// viewbag diğer tarafa taşıma yapıyor
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(TblUrunler p1)
        {
            var ktg = db.TblKategoriler.Where(m => m.KategoriId == p1.TblKategoriler.KategoriId).FirstOrDefault();
            p1.TblKategoriler = ktg;
            db.TblUrunler.Add(p1); // p1 vtye kayıt et insert into özelliği
            db.SaveChanges(); // değişiklikleri kaydet
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var urun = db.TblUrunler.Find(id);
            db.TblUrunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Guncelle(int id)
        {
            var urun = db.TblUrunler.Find(id);
            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriId.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;// viewbag diğer tarafa taşıma yapıyor
            return View("Guncelle", urun);
        }
        public ActionResult UrunGuncelle(TblUrunler p1)
        {
            var urun = db.TblUrunler.Find(p1.UrunId);
            urun.UrunAd = p1.UrunAd;
            urun.Marka = p1.Marka;
            // urun.UrunKategori = p1.UrunKategori;
            var ktg = db.TblKategoriler.Where(m => m.KategoriId == p1.TblKategoriler.KategoriId).FirstOrDefault();
            urun.UrunKategori = ktg.KategoriId;
            urun.Fiyat = p1.Fiyat;
            urun.Stok = p1.Stok;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}