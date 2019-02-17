using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PortfolioWebsite.Constants;

namespace PortfolioWebsite.Models
{
    public class Comment : Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text {
            get
            {
                if (Deleted)
                {
                    return "[removed]";
                }
                else
                {
                    return pText;
                }
            }
            set
            {
                pText = value;
            }
        }
        private string pText;

        public bool Edited { get; set; } = false;
        public bool Deleted { get; set; } = false;

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime EditDate { get; set; }
        public DateTime DeleteDate { get; set; }

        public IList<Comment> Replies { get; set; }

        [HiddenInput]
        [ForeignKey("ParentID")]
        public int? ParentID { get; set; } = null;
        public Comment Parent { get; set; }

        [Required]
        [HiddenInput]
        [ForeignKey("Work")]
        public int WorkID { get; set; }
        public Work Work { get; set; }

        [Required]
        [HiddenInput]
        [ForeignKey("User")]
        public string UserID { get; set; }
        public AppUser User { get; set; }

        public bool IsRoot()
        {
            return ParentID == null;
        }

        new public Comment Clone()
        {
            return (Comment)base.Clone();
        }
    }
}
