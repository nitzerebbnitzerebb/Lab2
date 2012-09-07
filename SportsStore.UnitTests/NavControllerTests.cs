using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using Moq;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class NavControllerTests
    {
        [TestMethod]
        public void NavController_Menu_IndicatesSelectedCategory()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Apples" },
                new Product {ProductID = 3, Name = "P3", Category = "Plums" },

            }.AsQueryable());
            NavController controller = new NavController(mock.Object);
            string CategoryToSelect = "Apples";

            // Act
            var result = controller.Menu(CategoryToSelect).ViewBag.SelectedCategory;

            // Assert
            Assert.AreEqual<string>(CategoryToSelect, result);
        }

        [TestMethod]
        public void NavController_Menu_CanCreateCategories()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Apples" },
                new Product {ProductID = 2, Name = "P2", Category = "Apples" },
                new Product {ProductID = 3, Name = "P3", Category = "Plums" },
                new Product {ProductID = 4, Name = "P4", Category = "Oranges" }

            }.AsQueryable());
            NavController controller = new NavController(mock.Object);

            // Act
            var result = ((IEnumerable<string>)controller.Menu().Model).ToArray();

            // Assert
            Assert.AreEqual<int>(result.Length, 3);
            Assert.AreEqual<string>(result[0], "Apples");
            Assert.AreEqual<string>(result[1], "Oranges");
            Assert.AreEqual<string>(result[2], "Plums");
        }
    }
}
