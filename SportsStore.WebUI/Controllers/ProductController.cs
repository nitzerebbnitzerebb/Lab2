using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        //
        // Get /Product/List/

        public ViewResult List()
        {
            return View(_repo.Products);
        }

        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }

    }
}
