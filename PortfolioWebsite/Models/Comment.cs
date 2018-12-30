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
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; }

        public bool Deleted { get; set; } = false;
        public bool Edited { get; set; } = false;

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime EditDate { get; set; }

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

        public bool IsAuthorizedEditor(ClaimsPrincipal user, IAuthorizationService authorizationService)
        {
            Claim userID = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userID != null)
            {
                return userID.Value == UserID || authorizationService.AuthorizeAsync(user, Policies.IS_ADMIN).Result.Succeeded;
            }
            else
            {
                return false;
            }
        }
    }
}
