using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AdminControllerTests
    {
        [TestMethod]
        public void AdminController_Delete_CannotDeleteInvalidProduct()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                new Product {ProductID = 3, Name = "P3", Category = "Cat1" },

            }.AsQueryable());
            var target = new AdminController(mock.Object);

            // Act
            target.Delete(100);

            // Assert
            mock.Verify(m => m.Delete(It.IsAny<Product>()), Times.Never());
        }

        [TestMethod]
        public void AdminController_Delete_CanDeleteProduct()
        {
            // Arrange
            var prod = new Product {ProductID = 2, Name = "P2", Category = "Cat2" };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1" },
                prod,
                new Product {ProductID = 3, Name = "P3", Category = "Cat1" },

            }.AsQueryable());
            var target = new AdminController(mock.Object);

            // Act
            target.Delete(prod.ProductID);

            // Assert
            mock.Verify(m => m.Delete(prod));
        }

        [TestMethod]
        public void AdminController_Edit_CannotSaveInvalidChanges()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            var target = new AdminController(mock.Object);
            Product newProduct = new Product { Name = "Test" };
            target.ModelState.AddModelError("error", "error");

            // Act
            var result = target.Edit(newProduct, null);

            // Assert
            mock.Verify(m => m.Save(It.IsAny<Product>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void AdminController_Edit_CanSaveValidChanges()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            var target = new AdminController(mock.Object);
            Product newProduct = new Product { Name = "Test" };

            // Act
            var result = target.Edit(newProduct, null);

            // Assert
            mock.Verify(m => m.Save(newProduct));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void AdminController_Edit_CannotEditNonexistingProduct()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product {ProductID = 2, Name = "P2", Category = "Cat2" },
                new Product {ProductID = 3, Name = "P3", Category = "Cat1" },

            }.AsQueryable());
            var target = new AdminController(mock.Object);

            // Act
            var result = target.Edit(4).ViewData.Model as Product;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void AdminController_Edit_CanEditProduct()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product {ProductID = 2, Name = "P2", Category = "Cat2" },
                new Product {ProductID = 3, Name = "P3", Category = "Cat1" },

            }.AsQueryable());
            var target = new AdminController(mock.Object);

            // Act
            var p1 = target.Edit(1).ViewData.Model as Product;
            var p2 = target.Edit(2).ViewData.Model as Product;
            var p3 = target.Edit(3).ViewData.Model as Product;

            // Assert
            Assert.AreEqual<int>(1, p1.ProductID);
            Assert.AreEqual<int>(2, p2.ProductID);
            Assert.AreEqual<int>(3, p3.ProductID);
        }

        [TestMethod]
        public void AdminController_Index_ContainsAllProducts() {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product {ProductID = 2, Name = "P2", Category = "Cat2" },
                new Product {ProductID = 3, Name = "P3", Category = "Cat1" },

            }.AsQueryable());
            var target = new AdminController(mock.Object);

            // Act
            var result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

            // Assert
            Assert.AreEqual<int>(result.Length, 3);
            Assert.AreEqual<string>("P1", result[0].Name);
            Assert.AreEqual<string>("P2", result[1].Name);
            Assert.AreEqual<string>("P3", result[2].Name);
        }
    }
}
