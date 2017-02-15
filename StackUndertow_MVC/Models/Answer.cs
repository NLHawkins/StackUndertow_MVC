using StackUndertow_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StackUndertow_MVC.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string AText { get; set; }
        public virtual ICollection<UpVote> UpVotes { get; set; }

        public string AOwnerId { get; set; }
        [ForeignKey("AOwnerId")]
        public virtual ApplicationUser AOwner { get; set; }

        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }


    }
}