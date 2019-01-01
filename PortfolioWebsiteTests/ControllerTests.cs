using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Data;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioWebsiteTests
{
    public class ControllerTests
    {
        private readonly DbContextOptionsBuilder<ApplicationDbContext> _contextOptionsBuilder;
        protected ApplicationDbContext _context;

        public ControllerTests()
        {
            _contextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            _contextOptionsBuilder.UseInMemoryDatabase("TestDB");
        }

        [SetUp]
        private void SetupContext()
        {
            _context = new ApplicationDbContext(_contextOptionsBuilder.Options);
        }

    }
}
