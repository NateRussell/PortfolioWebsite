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

        [Required]
        [HiddenInput]
        [ForeignKey("Work")]
        public int WorkID { get; set; }
        public Work Work { get; set; }

        [Required]
        [HiddenInput]
        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
