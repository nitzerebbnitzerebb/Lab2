using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Abstract;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.ViewModels;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void CartController_Checkout_CanCheckoutAndSubmitOrder()
        {
            // Arrange
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            var target = new CartController(null, mock.Object);

            // Act
            var result = target.Checkout(cart, new ShippingDetails());

            // Assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());
            Assert.AreEqual<string>("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void CartController_Checkout_CannotCheckoutInvalidShippingDetails()
        {
            // Arrange
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            var target = new CartController(null, mock.Object);
            target.ModelState.AddModelError("error", "error");

            // Act
            var result = target.Checkout(cart, new ShippingDetails());

            // Assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());
            Assert.AreEqual<string>("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void CartController_Checkout_CannotCheckoutEmptyCart()
        {
            // Arrange
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            ShippingDetails shippingDetails = new ShippingDetails();
            var target = new CartController(null, mock.Object);

            // Act
            var result = target.Checkout(cart, shippingDetails);

            // Assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());
            Assert.AreEqual<string>("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void CartController_AddToCart_CanAddToCart()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Apples" },

            }.AsQueryable());
            Cart cart = new Cart();
            var target = new CartController(mock.Object, null);

            // Act
            target.AddToCart(cart, 1, null);

            // Assert
            Assert.AreEqual<int>(cart.Lines.Count(), 1);
            Assert.AreEqual<int>(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }

        [TestMethod]
        public void CartController_AddToCart_GoesToCartScreen()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Apples" },

            }.AsQueryable());
            Cart cart = new Cart();
            var target = new CartController(mock.Object, null);

            // Act
            var result = target.AddToCart(cart, 1, "myUrl");

            // Assert
            Assert.AreEqual<string>(result.RouteValues["action"].ToString(), "Index");
            Assert.AreEqual<string>(result.RouteValues["returnUrl"].ToString(), "myUrl");
        }

        [TestMethod]
        public void CartController_Index_CanViewCartContents()
        {
            // Arrange
            Cart cart = new Cart();
            var target = new CartController(null, null);

            // Act
            var result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            // Assert
            Assert.AreEqual<Cart>(result.Cart, cart);
            Assert.AreEqual<string>(result.ReturnUrl, "myUrl");
        }
    }
}
