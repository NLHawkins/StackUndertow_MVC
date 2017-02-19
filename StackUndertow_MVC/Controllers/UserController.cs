using StackUndertow_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackUndertow_MVC.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string id)
        {
            ViewBag.Questions = db.Questions.Where(i => i.QOwnerId == id).ToList();
            ViewBag.Answers = db.Answers.Where(i => i.AOwnerId == id).ToList();
            ViewBag.UpVotesUser = db.UpVotes.Where(i => i.Answer.AOwnerId == id).Count();
            

            return View();
        }

        public ActionResult Profile(string id)
        {
            ViewBag.Person = db.Users.Where(i => i.Id == id).FirstOrDefault();
            ViewBag.Questions = db.Questions.Where(i => i.QOwnerId == id).ToList();
            ViewBag.Answers = db.Answers.Where(i => i.AOwnerId == id).ToList();
            ViewBag.UpVotesUser = db.UpVotes.Where(i => i.Answer.AOwnerId == id).Count();
            if (ViewBag.UpVotesUser == null)
            {
                ViewBag.UpVotesUser = "0";
            }


            return View();
        }

        
        
    }
}
    