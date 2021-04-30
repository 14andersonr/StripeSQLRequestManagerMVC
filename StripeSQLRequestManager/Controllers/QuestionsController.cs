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
    public class QuestionsController : Controller
    {

        // GET: Question
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new QuestionService(userId);
            var model = service.GetQuestions();

            return View(model);
        }

        //GET: Question/Create
        public ActionResult Create()
        {
            return View();
        }


        //POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateQuestionService();

            if (service.CreateQuestion(model))
            {
                TempData["SaveResult"] = "Your Question was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Question could not be created.");

            return View(model);
        }

        //GET: Question/Details/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateQuestionService();
            var model = svc.GetQuestionById(id);

            return View(model);
        }

        //GET: Question/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateQuestionService();
            var detail = service.GetQuestionById(id);
            var model =
                new QuestionEdit
                {
                    QuestionId = detail.QuestionId,
                    Content = detail.Content
                };
            return View(model);
        }

        //POST: Question/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, QuestionEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.QuestionId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateQuestionService();

            if (service.UpdateQuestion(model))
            {
                TempData["SaveResult"] = "Your Question was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Question could not be updated.");
            return View(model);
        }

        //GET: Question/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateQuestionService();
            var model = svc.GetQuestionById(id);

            return View(model);
        }

        //POST: Question/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateQuestionService();

            service.DeleteQuestion(id);

            TempData["SaveResult"] = "Your Question was deleted";

            return RedirectToAction("Index");
        }

        //Helper Method
        private QuestionService CreateQuestionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new QuestionService(userId);
            return service;
        }
    }
}
