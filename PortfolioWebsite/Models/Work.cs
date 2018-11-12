using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWebsite.Models
{
    public class Work
    {
        public int ID { get; set; }

        [Required]
        public int AccessLevel { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public int Type { get; set; }

        public string Tags { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        public List<Media> Media { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
