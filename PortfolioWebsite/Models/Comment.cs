using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWebsite.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [ForeignKey("Work")]
        public int WorkID { get; set; }
        public Work Work { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
