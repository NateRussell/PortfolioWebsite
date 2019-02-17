using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Data;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.AspNetCore.Authorization;
using PortfolioWebsite.Constants;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PortfolioWebsiteTests
{
    public class ControllerTests
    {
        private readonly DbContextOptionsBuilder<ApplicationDbContext> _contextOptionsBuilder;
        protected ApplicationDbContext _context = null;
        protected ServiceProvider _services = null;

        public ControllerTests()
        {
            _contextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            _contextOptionsBuilder.UseInMemoryDatabase("TestDB");
        }

        [SetUp]
        protected void SetupContext()
        {
            _context = new ApplicationDbContext(_contextOptionsBuilder.Options);
        }

        [SetUp]
        protected void SetupServices()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddAuthorization();
            services.AddLogging();
            services.AddOptions();
            Action<IServiceCollection> setupServices = null;
            setupServices?.Invoke(services);
            _services = services.BuildServiceProvider();
        }

        protected IAuthorizationService _authorization
        {
            get
            {
                return _services.GetRequiredService<IAuthorizationService>();
            }
        }

        protected IObjectModelValidator _objectValidator
        {
            get
            {
                Mock<IObjectModelValidator> objectValidator = new Mock<IObjectModelValidator>();
                objectValidator.Setup(o => o.Validate(
                    It.IsAny<ActionContext>(),
                    It.IsAny<ValidationStateDictionary>(),
                    It.IsAny<string>(),
                    It.IsAny<Object>()));
                return objectValidator.Object;
            }
        }

        protected ControllerContext CreateTestContext(string userName, string role)
        {
            return new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userName)
                    }, role))
                }
            };
        }
    }
}
