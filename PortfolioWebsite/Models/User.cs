using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PortfolioWebsite.Models
{
    public class User : IdentityUser
    {
        [Required]
        public bool OptIn { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Work> Works { get; set; }
    }
}
