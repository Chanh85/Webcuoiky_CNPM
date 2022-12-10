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
    public class cattablesController : Controller
    {
        private BanHangDB db = new BanHangDB();

        // GET: cattables
        public ActionResult Index()
        {
            return View(db.cattables.ToList());
        }

        // GET: cattables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cattable cattable = db.cattables.Find(id);
            if (cattable == null)
            {
                return HttpNotFound();
            }
            return View(cattable);
        }

        // GET: cattables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: cattables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "catid,catname,catdesc")] cattable cattable)
        {
            if (ModelState.IsValid)
            {
                db.cattables.Add(cattable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cattable);
        }

        // GET: cattables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cattable cattable = db.cattables.Find(id);
            if (cattable == null)
            {
                return HttpNotFound();
            }
            return View(cattable);
        }

        // POST: cattables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "catid,catname,catdesc")] cattable cattable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cattable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cattable);
        }

        // GET: cattables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cattable cattable = db.cattables.Find(id);
            if (cattable == null)
            {
                return HttpNotFound();
            }
            return View(cattable);
        }

        // POST: cattables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cattable cattable = db.cattables.Find(id);
            db.cattables.Remove(cattable);
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
