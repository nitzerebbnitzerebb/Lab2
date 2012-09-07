using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository _repo;

        public AdminController(IProductRepository repo)
        {
            _repo = repo;
        }

        public ViewResult Index()
        {
            return View(_repo.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = _repo.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

                _repo.Save(product);
                TempData["message"] = string.Format("{0} has been saved.", product.Name);
                return RedirectToAction("Index");
            }
            else
                return View(product);
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        public ActionResult Delete(int productId)
        {
            Product prod = _repo.Products.FirstOrDefault(p => p.ProductID == productId);
            if (null != prod)
            {
                _repo.Delete(prod);
                TempData["message"] = string.Format("{0} was deleted.", prod.Name);
            }
            return RedirectToAction("Index");
        }
    }
}
