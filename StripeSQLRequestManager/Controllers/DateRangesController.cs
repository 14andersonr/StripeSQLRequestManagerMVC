using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StripeSQL.Data;
using StripeSQL.Models;
using StripeSQL.Services;
using StripeSQLRequestManager.Models;

namespace StripeSQLRequestManager.Controllers
{
    [Authorize]
    public class DateRangesController : Controller
    {
        // GET: DateRange
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new DateRangeService(userId);
            var model = service.GetDateRange();

            return View(model);
        }

        //GET: DateRange/Create
        public ActionResult Create()
        {
            return View();
        }


        //POST: DateRange/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DateRangeCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateDateRangeService();

            if (service.CreateDateRange(model))
            {
                TempData["SaveResult"] = "Your Date Range was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Date Range could not be created.");

            return View(model);
        }

        //GET: DateRange/Details/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateDateRangeService();
            var model = svc.GetDateRangeById(id);

            return View(model);
        }

        //GET: DateRange/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateDateRangeService();
            var detail = service.GetDateRangeById(id);
            var model =
                new DateRangeEdit
                {
                    DateRangeId = detail.DateRangeId,
                    StartDate = detail.StartDate,
                    EndDate = detail.EndDate,
                };
            return View(model);
        }

        //POST: DateRange/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DateRangeEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.DateRangeId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateDateRangeService();

            if (service.UpdateDateRange(model))
            {
                TempData["SaveResult"] = "Your Date Range was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Date Range could not be updated.");
            return View(model);
        }

        //GET: DateRange/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateDateRangeService();
            var model = svc.GetDateRangeById(id);

            return View(model);
        }

        //POST: DateRange/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateDateRangeService();

            service.DeleteDateRange(id);

            TempData["SaveResult"] = "Your Date Range was deleted";

            return RedirectToAction("Index");
        }

        //Helper Method
        private DateRangeService CreateDateRangeService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new DateRangeService(userId);
            return service;
        }
    }
}
