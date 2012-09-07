using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.ViewModels;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repo;
        public int PageSize = 4; // We will change this later

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        //
        // Get /Product/List/

        public ViewResult List(int page = 1)
        {
            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = _repo.Products.OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = _repo.Products.Count() }
            };

            return View(viewModel);
        }

        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }

    }
}
