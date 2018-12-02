using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

        public DbSet<PortfolioWebsite.Models.Work> Work { get; set; }

        public DbSet<PortfolioWebsite.Models.Comment> Comment { get; set; }
    }
}
