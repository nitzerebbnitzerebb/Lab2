using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Cart_Clear_CanClearLines()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);

            // Act
            target.Clear();

            // Assert
            Assert.AreEqual<int>(target.Lines.Count(), 0);
        }

        [TestMethod]
        public void Cart_ComputeTotalValue_CanComputeTotalValue()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);

            // Act
            var result = target.ComputeTotalValue();

            // Assert
            Assert.AreEqual<decimal>(result, 450M);
        }

        [TestMethod]
        public void Cart_RemoveLine_CanRemoveLines()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            // Act
            target.RemoveLine(p2);

            // Assert
            Assert.AreEqual<int>(target.Lines.Where(c => c.Product == p2).Count(), 0);
            Assert.AreEqual<int>(target.Lines.Count(), 2);
        }

        [TestMethod]
        public void Cart_AddItem_CanAddQuantityForExistingLines()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.AreEqual<int>(results.Length, 2);
            Assert.AreEqual<int>(results[0].Quantity, 11);
            Assert.AreEqual<int>(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Cart_AddItem_CanAddLines()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.AreEqual<int>(results.Length, 2);
            Assert.AreEqual<Product>(results[0].Product, p1);
            Assert.AreEqual<Product>(results[1].Product, p2);
        }
    }
}
