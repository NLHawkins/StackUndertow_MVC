using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StackUndertow_MVC.Models
{
    public class Question
    {
        public int Id { get; set; }
        [DisplayName("Title for your Question")]
        public string QTitle { get; set; }
        [DisplayName("What's Your Question?")]
        public string QText { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

        public string QOwnerName { get; set; }
        public string QOwnerId { get; set; }
        [ForeignKey("QOwnerId")]
        public virtual ApplicationUser QOwner { get; set; }

    }
}