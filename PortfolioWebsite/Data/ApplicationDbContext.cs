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
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Work>()
                .HasMany(p => p.Comments)
                .WithOne(b => b.Work);
            base.OnModelCreating(modelBuilder);
        }
        */
        public virtual DbSet<AppUser> User { get; set; }

        public DbSet<PortfolioWebsite.Models.Work> Work { get; set; }

        public DbSet<PortfolioWebsite.Models.Comment> Comment { get; set; }
    }
}
