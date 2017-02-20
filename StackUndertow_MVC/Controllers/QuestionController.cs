using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StackUndertow_MVC.Models;
using Microsoft.AspNet.Identity;

namespace StackUndertow_MVC.Controllers
{
    public class QuestionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Index()
        {
            ViewBag.AllQuestions = db.Questions.ToList();
            return View();
        }

        // GET: Question/Details/5
        public ActionResult Details(int QId)
        {
            Question question = db.Questions.Find(QId);
            var userInstance = db.Users.Where(i => i.Id == question.QOwnerId);
            if (question == null)
            {
                return HttpNotFound();
            }

            return View(question);
        }


        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QTitle,QText")] Question question)
        {
            if (ModelState.IsValid)
            {   
                
                var userId = User.Identity.GetUserId();
                var userName = User.Identity.GetUserName();
                var userInstance = db.Users.Where(i => i.Id == userId).FirstOrDefault();
                question.QOwnerName = userName;
                question.QOwnerId = userId;
                question.FinalAnswer = false;
                userInstance.UScore += 5;
                db.Questions.Add(question);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(question);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Choose(int Qid, int Aid)
        {
            if (Qid == 0 || Aid == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Question question = db.Questions.Find(Qid);
            Answer answer = db.Answers.Find(Aid);
            ApplicationUser winner = db.Users.Where(i => i.Id == answer.AOwnerId).FirstOrDefault();
            if (question == null)
            {
                return HttpNotFound();
            }

            question.FinalAnswer = true;
            answer.Chosen = true;
            winner.UScore += 100;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,QTitle,QText,QOwnerId")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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
