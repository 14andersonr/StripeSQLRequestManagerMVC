using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StripeSQL.Data;
using StripeSQLRequestManager.Models;

namespace StripeSQLRequestManager.Controllers
{
    public class SQLCodesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SQLCodes
        public ActionResult Index()
        {
            var sQLCodes = db.SQLCodes.Include(s => s.DateRange).Include(s => s.Question);
            return View(sQLCodes.ToList());
        }

        // GET: SQLCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SQLCode sQLCode = db.SQLCodes.Find(id);
            if (sQLCode == null)
            {
                return HttpNotFound();
            }
            return View(sQLCode);
        }

        // GET: SQLCodes/Create
        public ActionResult Create()
        {
            ViewBag.DateRangeId = new SelectList(db.DateRanges, "DateRangeId", "DateRangeId");
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Content");
            return View();
        }

        // POST: SQLCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SQLCodeId,QuestionId,DateRangeId,OwnerId,SQL,ResolvedDate,Resolved,CreatedUtc,ModifiedUtc")] SQLCode sQLCode)
        {
            if (ModelState.IsValid)
            {
                db.SQLCodes.Add(sQLCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DateRangeId = new SelectList(db.DateRanges, "DateRangeId", "DateRangeId", sQLCode.DateRangeId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Content", sQLCode.QuestionId);
            return View(sQLCode);
        }

        // GET: SQLCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SQLCode sQLCode = db.SQLCodes.Find(id);
            if (sQLCode == null)
            {
                return HttpNotFound();
            }
            ViewBag.DateRangeId = new SelectList(db.DateRanges, "DateRangeId", "DateRangeId", sQLCode.DateRangeId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Content", sQLCode.QuestionId);
            return View(sQLCode);
        }

        // POST: SQLCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SQLCodeId,QuestionId,DateRangeId,OwnerId,SQL,ResolvedDate,Resolved,CreatedUtc,ModifiedUtc")] SQLCode sQLCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sQLCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DateRangeId = new SelectList(db.DateRanges, "DateRangeId", "DateRangeId", sQLCode.DateRangeId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Content", sQLCode.QuestionId);
            return View(sQLCode);
        }

        // GET: SQLCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SQLCode sQLCode = db.SQLCodes.Find(id);
            if (sQLCode == null)
            {
                return HttpNotFound();
            }
            return View(sQLCode);
        }

        // POST: SQLCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SQLCode sQLCode = db.SQLCodes.Find(id);
            db.SQLCodes.Remove(sQLCode);
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
