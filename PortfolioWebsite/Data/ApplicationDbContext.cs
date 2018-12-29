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
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>()
                .HasMany(p => p.Replies)
                .WithOne(b => b.Parent)
                .HasForeignKey("ParentID")
                .OnDelete(DeleteBehavior.Restrict);
        }
        
        public virtual DbSet<AppUser> User { get; set; }

        public DbSet<PortfolioWebsite.Models.Work> Work { get; set; }

        public DbSet<PortfolioWebsite.Models.Comment> Comment { get; set; }
    }
}
