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
            var qInstance = db.Questions.Where(i => i.Id == QId).FirstOrDefault();
            ViewBag.Answers = db.Answers.Where(i => i.QuestionId == QId);
            ViewBag.QuestionText = qInstance.QText;
            ViewBag.Question = qInstance;
            return View();
        }

        public ActionResult Create(int QId)
        {
            var qInstance = db.Questions.Where(i => i.Id == QId).FirstOrDefault();
            ViewBag.QuestionText = qInstance.QText;
            ViewBag.Question = qInstance;
            return View();
        }



        public ActionResult Details(int AId)
        {
            Answer answer = db.Answers.Find(AId);
            var userInstance = db.Users.Where(i => i.Id == answer.AOwnerId);
            if (answer == null)
            {
                return HttpNotFound();
            }

            return View(answer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AText, QuestionId")] Answer answer)
        {

            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();
            answer.AOwnerId = userId;
            answer.AOwnerName = userName;
            db.Answers.Add(answer);
            db.SaveChanges();
            return RedirectToAction("UploadAPic","Upload", new { AId = answer.Id });
        }

        public ActionResult UpVote(int AId)
        {

            var userId = User.Identity.GetUserId();
            var aInstance = db.Answers.Where(i => i.Id == AId).FirstOrDefault();
            var upvote = new UpVote();
            bool notOwnerCk = new bool();
            if (aInstance.AOwnerId == userId)
            {
                notOwnerCk = false;
            }
            else
            {
                notOwnerCk = true;
            }
            var upVoterTwiceCk = db.UpVotes.Where(i => i.Answer.Id == AId && i.VoterId == userId).Any();                  
            if (notOwnerCk && !upVoterTwiceCk)
            {
                aInstance.AOwner.UScore += 10;
                aInstance.AScore++;
                upvote.AnswerId = AId;
                upvote.VoterId = User.Identity.GetUserId();
                db.UpVotes.Add(upvote);
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Question", new { QId = aInstance.QuestionId });
        }

        public ActionResult DownVote(int AId)
        {

            var userId = User.Identity.GetUserId();
            var userInstance = db.Users.Where(i => i.Id == userId).FirstOrDefault();
            var aInstance = db.Answers.Where(i => i.Id == AId).FirstOrDefault();
            var upvote = new UpVote();
            bool notOwnerCk = new bool();
            if (aInstance.AOwnerId == userId)
            {
                notOwnerCk = false;
            }
            else
            {
                notOwnerCk = true;
            }
            var upVoterTwiceCk = db.UpVotes.Where(i => i.Answer.Id == AId && i.VoterId == userId).Any();
            if (notOwnerCk && !upVoterTwiceCk)
            {
                aInstance.AOwner.UScore -= 5;
                aInstance.AScore--;
                userInstance.UScore--;
                upvote.AnswerId = AId;
                upvote.VoterId = User.Identity.GetUserId();
                db.UpVotes.Add(upvote);
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Question", new { QId = aInstance.QuestionId });
        }

    }
}