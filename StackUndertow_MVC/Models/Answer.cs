using Microsoft.AspNet.Identity;
using StackUndertow_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StackUndertow_MVC.Models
{
    public class Answer
    {
        public int Id { get; set; }
        [DisplayName("Answer")]
        public string AText { get; set; }
        public virtual ICollection<UpVote> UpVotes { get; set; }
        public string AOwnerName { get; set; }
        public int AScore { get; set; }
        public bool Chosen { get; set; }
        public virtual ICollection<ImageUpload> File { get; set; }


        public string AOwnerId { get; set; }
        [ForeignKey("AOwnerId")]
        public virtual ApplicationUser AOwner { get; set; }

        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        public bool HasVoted(string userId)
        {
            foreach (var u in UpVotes)
            {
                if (u.VoterId == userId)
                {
                    return true;
                }
            }

            return false;
        }

        public bool OwnsAnswer(string userId)
        {
            if(AOwnerId == userId)
            {
                return true;
            }
            return false;
        }


    }
}