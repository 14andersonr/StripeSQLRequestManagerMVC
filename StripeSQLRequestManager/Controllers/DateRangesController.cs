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
    public class DateRangesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DateRanges
        public ActionResult Index()
        {
            var dateRanges = db.DateRanges.Include(d => d.Question);
            return View(dateRanges.ToList());
        }

        // GET: DateRanges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateRange dateRange = db.DateRanges.Find(id);
            if (dateRange == null)
            {
                return HttpNotFound();
            }
            return View(dateRange);
        }

        // GET: DateRanges/Create
        public ActionResult Create()
        {
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Content");
            return View();
        }

        // POST: DateRanges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DateRangeId,QuestionId,OwnerId,StartDate,EndDate,CreatedUtc,ModifiedUtc")] DateRange dateRange)
        {
            if (ModelState.IsValid)
            {
                db.DateRanges.Add(dateRange);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Content", dateRange.QuestionId);
            return View(dateRange);
        }

        // GET: DateRanges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateRange dateRange = db.DateRanges.Find(id);
            if (dateRange == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Content", dateRange.QuestionId);
            return View(dateRange);
        }

        // POST: DateRanges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DateRangeId,QuestionId,OwnerId,StartDate,EndDate,CreatedUtc,ModifiedUtc")] DateRange dateRange)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dateRange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Content", dateRange.QuestionId);
            return View(dateRange);
        }

        // GET: DateRanges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateRange dateRange = db.DateRanges.Find(id);
            if (dateRange == null)
            {
                return HttpNotFound();
            }
            return View(dateRange);
        }

        // POST: DateRanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DateRange dateRange = db.DateRanges.Find(id);
            db.DateRanges.Remove(dateRange);
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
