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
    public class producttablesController : Controller
    {
        private BanHangDB db = new BanHangDB();

        // GET: producttables
        public ActionResult Index()
        {
            return View(db.producttables.ToList());
        }

        // GET: producttables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producttable producttable = db.producttables.Find(id);
            if (producttable == null)
            {
                return HttpNotFound();
            }
            return View(producttable);
        }

        // GET: producttables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: producttables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productid,productname,productquantity,productprice,productcategory")] producttable producttable)
        {
            if (ModelState.IsValid)
            {
                db.producttables.Add(producttable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producttable);
        }

        // GET: producttables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producttable producttable = db.producttables.Find(id);
            if (producttable == null)
            {
                return HttpNotFound();
            }
            return View(producttable);
        }

        // POST: producttables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productid,productname,productquantity,productprice,productcategory")] producttable producttable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producttable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producttable);
        }

        // GET: producttables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producttable producttable = db.producttables.Find(id);
            if (producttable == null)
            {
                return HttpNotFound();
            }
            return View(producttable);
        }

        // POST: producttables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            producttable producttable = db.producttables.Find(id);
            db.producttables.Remove(producttable);
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
