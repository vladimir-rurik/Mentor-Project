using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MentorProject.WebUI.Controllers;
using MentorProject.WebUI.Infrastructure.Abstract;
using MentorProject.WebUI.Models;

namespace MentorProject.UnitTests {
    [TestClass]
    public class AdminSecurityTests {

        [TestMethod]
        public void Can_Login_With_Valid_Credentials() {

            // Arrange - create a mock Authorization provider
            Mock<IAuthorizationManager> mockAuth = new Mock<IAuthorizationManager>();
            mockAuth.Setup(m => m.Login("admin", "secret")).Returns(true);

            // Arrange - create the view model
            LoginViewModel model = new LoginViewModel {
                UserName = "admin",
                Password = "secret"
            };

			// Arrange - create the controller
			Mock<ICookieSetup> mockCookie = new Mock<ICookieSetup>();
            AccountController target = new AccountController(mockAuth.Object, mockCookie.Object);

            // Act - authenticate using valid credentials
            ActionResult result = target.Login(model, "/MyURL");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials() {

            // Arrange - create a mock Authorization provider
            Mock<IAuthorizationManager> mockAuth = new Mock<IAuthorizationManager>();
            mockAuth.Setup(m => m.Login("badUser", "badPass")).Returns(false);

            // Arrange - create the view model
            LoginViewModel model = new LoginViewModel {
                UserName = "badUser",
                Password = "badPass"
            };

			// Arrange - create the controller
			Mock<ICookieSetup> mockCookie = new Mock<ICookieSetup>();
			AccountController target = new AccountController( mockAuth.Object, mockCookie.Object );

			// Act - authenticate using valid credentials
			ActionResult result = target.Login(model, "/MyURL");

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
