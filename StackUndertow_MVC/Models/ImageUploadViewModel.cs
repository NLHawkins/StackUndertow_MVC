using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackUndertow_MVC.Models
{
    public class ImageUploadViewModel
    {
        [Required]
        public HttpPostedFile File { get; set; }

    }
}