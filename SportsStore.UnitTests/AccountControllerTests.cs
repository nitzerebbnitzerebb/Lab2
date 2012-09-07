using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.Infrastructure.Abstract;
using Moq;
using SportsStore.WebUI.ViewModels;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void AccountController_Logon_CannotLoginWithInvalidCredentials()
        {
            //Arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);
            var model = new LogonViewModel
            {
                UserName = "badUser",
                Password = "basPass"
            };
            var target = new AccountController(mock.Object);

            // Act
            var result = target.LogOn(model, "/MyUrl");

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void AccountController_Logon_CanLoginWithValidCredentials() {
            //Arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);
            var model = new LogonViewModel
            {
                UserName = "admin",
                Password = "secret"
            };
            var target = new AccountController(mock.Object);

            // Act
            var result = target.LogOn(model, "/MyUrl");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual<string>("/MyUrl", ((RedirectResult)result).Url);
        }
    }
}
