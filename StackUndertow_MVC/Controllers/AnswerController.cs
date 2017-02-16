using Microsoft.AspNet.Identity;
using StackUndertow_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackUndertow_MVC.Controllers
{
    public class AnswerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int QId)
        {
            return View();
        }

        public ActionResult Create(int QId)
        {
            var qInstance = db.Questions.Where(i => i.Id == QId).FirstOrDefault();
            ViewBag.Question = qInstance.QText;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AText, QuestionId")] Answer answer)
        {

                var userId = User.Identity.GetUserId();
                var userName = User.Identity.GetUserName();
            answer.QuestionId = int.Parse(Request.Form["QuestionId"]);
                answer.AOwnerName = userName;
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Index");



        }

    }
}