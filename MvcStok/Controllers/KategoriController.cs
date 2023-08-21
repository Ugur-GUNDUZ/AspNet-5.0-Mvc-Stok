using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.entity;
using PagedList;
using PagedList.Mvc;


namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
           
           
            //var kategorilerlist = db.TblKategoriler.ToList();
            var kategorilerlist = db.TblKategoriler.ToList().ToPagedList(sayfa, 2);
            return View(kategorilerlist);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(TblKategoriler p1)
        {
            if (!ModelState.IsValid) {
                return View("YeniKategori");
            }
            db.TblKategoriler.Add(p1); // p1 vtye kayıt et insert into özelliği
            db.SaveChanges(); // değişiklikleri kaydet
            return View();
        }
        public ActionResult Sil(int id)
        {
            var kategori = db.TblKategoriler.Find(id);
            db.TblKategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Guncelle(int id)
        {
            var kategori = db.TblKategoriler.Find(id);
            return View("Guncelle",kategori);
        }
        public ActionResult KategoriGuncelle(TblKategoriler p1)
        {
            var kategori = db.TblKategoriler.Find(p1.KategoriId);
            kategori.KategoriAd = p1.KategoriAd;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}