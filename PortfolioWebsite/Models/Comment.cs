using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWebsite.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; }

        public bool Deleted { get; set; } = false;
        public bool Edited { get; set; } = false;

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime EditDate { get; set; }

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
    }
}
