using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PortfolioWebsite.Models
{
    public static class Roles
    {
        public const string USER = "User";
        public const string ADMIN = "Admin";
    }

    public class AppUser : IdentityUser
    {
        
        [Required]
        public bool OptIn { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<Work> Works { get; set; }
    }
}
