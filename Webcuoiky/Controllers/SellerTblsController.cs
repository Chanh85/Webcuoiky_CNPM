using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webcuoiky.Models;

namespace Webcuoiky.Controllers
{
    [Authorize(Roles = "admin")]
    public class SellerTblsController : Controller
    {
        private BanHangDB db = new BanHangDB();

        // GET: SellerTbls
        public ActionResult Index()
        {
            return View(db.SellerTbls.ToList());
        }

        // GET: SellerTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellerTbl sellerTbl = db.SellerTbls.Find(id);
            if (sellerTbl == null)
            {
                return HttpNotFound();
            }
            return View(sellerTbl);
        }

        // GET: SellerTbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SellerTbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SellerId,SellerName,SellerAge,SellerPhone")] SellerTbl sellerTbl)
        {
            if (ModelState.IsValid)
            {
                db.SellerTbls.Add(sellerTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sellerTbl);
        }

        // GET: SellerTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellerTbl sellerTbl = db.SellerTbls.Find(id);
            if (sellerTbl == null)
            {
                return HttpNotFound();
            }
            return View(sellerTbl);
        }

        // POST: SellerTbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SellerId,SellerName,SellerAge,SellerPhone")] SellerTbl sellerTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sellerTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sellerTbl);
        }

        // GET: SellerTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellerTbl sellerTbl = db.SellerTbls.Find(id);
            if (sellerTbl == null)
            {
                return HttpNotFound();
            }
            return View(sellerTbl);
        }

        // POST: SellerTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SellerTbl sellerTbl = db.SellerTbls.Find(id);
            db.SellerTbls.Remove(sellerTbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
