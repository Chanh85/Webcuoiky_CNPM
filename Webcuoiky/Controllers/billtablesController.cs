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
    public class billtablesController : Controller
    {
        private BanHangDB db = new BanHangDB();

        // GET: billtables
        public ActionResult Index()
        {
            var billtables = db.billtables.Include(b => b.tbl_user);
            return View(billtables.ToList());
        }

        // GET: billtables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            billtable billtable = db.billtables.Find(id);
            if (billtable == null)
            {
                return HttpNotFound();
            }
            return View(billtable);
        }

        // GET: billtables/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.tbl_user, "id", "name");
            return View();
        }

        // POST: billtables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "billid,date,user_id,total_bill")] billtable billtable)
        {
            if (ModelState.IsValid)
            {
                db.billtables.Add(billtable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.tbl_user, "id", "name", billtable.user_id);
            return View(billtable);
        }

        // GET: billtables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            billtable billtable = db.billtables.Find(id);
            if (billtable == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.tbl_user, "id", "name", billtable.user_id);
            return View(billtable);
        }

        // POST: billtables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "billid,date,user_id,total_bill")] billtable billtable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billtable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.tbl_user, "id", "name", billtable.user_id);
            return View(billtable);
        }

        // GET: billtables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            billtable billtable = db.billtables.Find(id);
            if (billtable == null)
            {
                return HttpNotFound();
            }
            return View(billtable);
        }

        // POST: billtables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            billtable billtable = db.billtables.Find(id);
            db.billtables.Remove(billtable);
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
