using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PortfolioWebsite.Controllers;
using PortfolioWebsite.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PortfolioWebsite.Constants;

namespace PortfolioWebsiteTests
{
    [TestFixture]
    public class CommentsControllerTests : ControllerTests
    {
        [Test]
        public void Post_Create_ReturnsView()
        {
            /*
            //Arrange
            CommentsController controller = new CommentsController(_context, _authorization);
            controller.ControllerContext = CreateTestContext("TestUser", Roles.ADMIN);
            controller.ObjectValidator = _objectValidator;

            //Act
            IActionResult result = (controller.Create(new Comment() { Text = "Test Comment", WorkID = 1 })).Result;

            //Assert
            Assert.IsInstanceOf(typeof(RedirectToActionResult), result);
            */
        }
    }
}
