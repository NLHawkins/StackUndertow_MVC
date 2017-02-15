using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StackUndertow_MVC.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QTitle { get; set; }
        public string QText { get; set; }
        public virtual ICollection<Answer> Answer { get; set; }

        public string QOwnerId { get; set; }
        [ForeignKey("QOwnerId")]
        public virtual ApplicationUser QOwner { get; set; }
    }
}