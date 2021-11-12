using Microsoft.AspNetCore.Mvc;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Services;
using System.Collections.Generic;

namespace P2FixAnAppDotNetCode.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILanguageService _languageService;

        public ProductController(IProductService productService, ILanguageService languageService)
        {
            _productService = productService;
            _languageService = languageService;
        }

        public IActionResult Index()
        {
            // NOTE: 
            // The products variable has had its type changed from a Product[] to a List<Product>.
            // The view itself has not been changed because it's expecting an IEnumerable<Product>
            // and works either way.
            List<Product> products = _productService.GetAllProducts();
            return View(products);
        }
    }
}