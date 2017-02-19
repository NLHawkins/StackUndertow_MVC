using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StackUndertow_MVC.Models
{

    public class ImageUpload
    {
        public int Id { get; set; }
        public string File { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public int QId { get; set; }
        [ForeignKey("QId")]
        public virtual Question Question { get; set; }

        public int AId { get; set; }
        [ForeignKey("AId")]
        public virtual Answer Answer { get; set; }

        public virtual string FilePath
        {
            get
            {
                return $"/Uploads/{File}";
            }
        }
    }
}