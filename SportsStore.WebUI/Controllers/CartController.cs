using System;
using SportsStore.Domain.Abstract;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.ViewModels;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repo;
        private IOrderProcessor _orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor proc) {
            _repo = repo;
            _orderProcessor = proc;
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
                ModelState.AddModelError("", "Sorry, your cart is empty.");

            if (ModelState.IsValid)
            {
                _orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
                return View(shippingDetails);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        public ViewResult Summary(Cart cart)
        {
            return View("_Summary", cart);
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _repo.Products.FirstOrDefault(p => p.ProductID == productId);

            if (null != product)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _repo.Products.FirstOrDefault(p => p.ProductID == productId);

            if (null != product)
            {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (null == cart)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

    }
}
