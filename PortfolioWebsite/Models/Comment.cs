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

        public int WorkID { get; set; }
        [ForeignKey("WorkID")]
        public Media Work { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
