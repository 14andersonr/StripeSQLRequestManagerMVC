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
    public class SQLCodesController : Controller
    {
        // GET: SQLCode
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SQLCodeService(userId);
            var model = service.GetSQLCodes();

            return View(model);
        }

        //GET: SQLCode/Create
        public ActionResult Create()
        {

            var service = CreateSQLCodeService();
            //Assigning generic list to ViewBag
            ViewBag.Questions = service.QuestionsForSQLCode();

            //Assigning generic list to ViewBag
            ViewBag.DateRanges = service.DateRangeForSQLCode();

            return View();
        }


        //POST: SQLCode/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SQLCodeCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateSQLCodeService();

            if (service.CreateSQLCode(model))
            {
                TempData["SaveResult"] = "Your SQL Code was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "SQL Code could not be created.");

            return View(model);
        }

        //GET: SQLCode/Details/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateSQLCodeService();
            var model = svc.GetSQLCodeById(id);

            return View(model);
        }

        //GET: SQLCode/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateSQLCodeService();
            var detail = service.GetSQLCodeById(id);
            var model =
                new SQLCodeEdit
                {
                    SQLCodeId = detail.SQLCodeId,
                    SQL = detail.SQL,
                    ResolvedDate = detail.ResolvedDate,
                    Resolved = detail.Resolved

                };
            return View(model);
        }

        //POST: SQLCode/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SQLCodeEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.SQLCodeId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateSQLCodeService();

            if (service.UpdateSQLCode(model))
            {
                TempData["SaveResult"] = "Your SQL Code was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your SQL Code could not be updated.");
            return View(model);
        }

        //GET: SQLCode/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateSQLCodeService();
            var model = svc.GetSQLCodeById(id);

            return View(model);
        }

        //POST: SQLCode/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateSQLCodeService();

            service.DeleteSQLCode(id);

            TempData["SaveResult"] = "Your SQL Code was deleted";

            return RedirectToAction("Index");
        }

        //Helper Method
        private SQLCodeService CreateSQLCodeService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SQLCodeService(userId);
            return service;
        }
    }
}
