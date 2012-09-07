using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.ViewModels;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void ProductController_List_CanGenerateCategorySpecificCount()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product {ProductID = 2, Name = "P2", Category = "Cat2" },
                new Product {ProductID = 3, Name = "P3", Category = "Cat1" },
                new Product {ProductID = 4, Name = "P4", Category = "Cat2" },
                new Product {ProductID = 5, Name = "P5", Category = "Cat3" },

            }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            var result1 = ((ProductListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            var result2 = ((ProductListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            var result3 = ((ProductListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            var resultAll = ((ProductListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            // Assert
            Assert.AreEqual<int>(result1, 2);
            Assert.AreEqual<int>(result2, 2);
            Assert.AreEqual<int>(result3, 1);
            Assert.AreEqual<int>(resultAll, 5);
        }

        [TestMethod]
        public void ProductController_List_CanFilterProducts()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product {ProductID = 2, Name = "P2", Category = "Cat2" },
                new Product {ProductID = 3, Name = "P3", Category = "Cat1" },
                new Product {ProductID = 4, Name = "P4", Category = "Cat2" },
                new Product {ProductID = 5, Name = "P5", Category = "Cat3" },

            }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            var result = (ProductListViewModel)controller.List("Cat2", 1).Model;

            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.AreEqual<int>(prodArray.Length, 2);
            Assert.IsTrue(prodArray[0].Name == "P2" && prodArray[0].Category == "Cat2");
            Assert.IsTrue(prodArray[1].Name == "P4" && prodArray[1].Category == "Cat2");
        }

        [TestMethod]
        public void ProductController_List_CanSendPageInfo()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" },
                new Product {ProductID = 4, Name = "P4" },
                new Product {ProductID = 5, Name = "P5" },

            }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            PagingInfo result = ((ProductListViewModel)controller.List(null, 2).Model).PagingInfo;

            // Assert
            Assert.AreEqual<int>(result.CurrentPage, 2);
            Assert.AreEqual<int>(result.ItemsPerPage, 3);
            Assert.AreEqual<int>(result.TotalItems, 5);
            Assert.AreEqual<int>(result.TotalPages, 2);
        }

        [TestMethod]
        public void ProductController_List_CanPaginate()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" },
                new Product {ProductID = 4, Name = "P4" },
                new Product {ProductID = 5, Name = "P5" },

            }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Model;

            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual<string>(prodArray[0].Name, "P4");
            Assert.AreEqual<string>(prodArray[1].Name, "P5");
        }
    }
}
