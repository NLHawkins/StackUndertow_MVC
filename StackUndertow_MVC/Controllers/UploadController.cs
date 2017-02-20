using Microsoft.AspNet.Identity;
using StackUndertow_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackUndertow_MVC.Controllers
{
    public class UploadController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Upload()
        {
            var ImageUploadViewModel = new ImageUploadViewModel();
            return View(ImageUploadViewModel);
        }

        [HttpPost]
        public ActionResult UploadQPic(ImageUploadViewModel formData)
        {
            var uploadedFile = Request.Files[0];
            var qid = int.Parse(Request.Form["QId"]);
            string filename = $"{DateTime.Now.Ticks}{uploadedFile.FileName}";
            var serverPath = Server.MapPath(@"~\Uploads");
            var fullPath = Path.Combine(serverPath, filename);
            uploadedFile.SaveAs(fullPath);
            

            var uploadModel = new ImageUpload
            {   
                File = filename,
                QId = qid,
                Created = DateTime.Now
            };
            
            db.ImageUploads.Add(uploadModel);
            db.SaveChanges();
            return View();
        }

        [HttpPost]
        public ActionResult UploadAPic(ImageUploadViewModel formData)
        {
            var uploadedFile = Request.Files[0];
            var aid = int.Parse(Request.Form["AId"]);
            string filename = $"{DateTime.Now.Ticks}{uploadedFile.FileName}";
            var serverPath = Server.MapPath(@"~\Uploads");
            var fullPath = Path.Combine(serverPath, filename);
            uploadedFile.SaveAs(fullPath);


            var uploadModel = new ImageUpload
            {
                File = filename,
                AId = aid,
                Created = DateTime.Now
            };

            db.ImageUploads.Add(uploadModel);
            db.SaveChanges();
            return View();
        }

        public ActionResult UploadProfilePic(string userId)
        {
            var ImageUploadViewModel = new ImageUploadViewModel();
            ViewBag.UserId = userId;
            return View(ImageUploadViewModel);
        }

        public ActionResult UploadQPic(int QId)
        {
            var ImageUploadViewModel = new ImageUploadViewModel();
            ViewBag.UserId = User.Identity.GetUserId();
            var ImageUploads = db.ImageUploads.Where(i => i.QId == QId).ToList().OrderByDescending(c=>c.Created);
            ImageUploads.ToList();
            //ViewBag.LastUpload = ImageUploads[0];



            return View(ImageUploadViewModel);
        }

        public ActionResult UploadAPic(int AId)
        {
            var ImageUploadViewModel = new ImageUploadViewModel();
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.ImageUpload = db.ImageUploads.Where(i => i.AId == AId).FirstOrDefault();
            return View(ImageUploadViewModel);
        }

        [HttpPost]
        public ActionResult UploadProfilePic(ImageUploadViewModel formData)
        {
            var userId = Request.Form["UserId"];
            var uploadedFile = Request.Files[0];
            string filename = $"{DateTime.Now.Ticks}{uploadedFile.FileName}";
            var serverPath = Server.MapPath(@"~\Uploads");
            var fullPath = Path.Combine(serverPath, filename);
            uploadedFile.SaveAs(fullPath);

            var uploadModel = new ImageUpload
            {
                File = filename,
                UserId = userId,
                Created = DateTime.Now
                
            };
            db.ImageUploads.Add(uploadModel);
            db.SaveChanges();
            return RedirectToAction("Profile","User",new { id = userId });
        }
    }
}
    