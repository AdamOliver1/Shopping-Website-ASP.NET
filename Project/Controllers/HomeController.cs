using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Filters;
using Project.Helpers;
using Project.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [ExceptionFilter]
    public class HomeController : Controller
    {
        IProductService _service;
        private ILogger _logger;

        public HomeController(IProductService service, ILogger<HomeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            var view = View(_service.GetAllUnperchasedAsync().Result.ToList());
            await CheckLastModified();
            return view;
        }

        public IActionResult BuyCart()
        {

            var name = HttpContext.GetUsername();
            if (name != null)
                _service.BuyCartUser(name);
            else
            {
                _service.BuyCartGuest(HttpContext.GetProductsInCart());
                HttpContext.DeleteProductsInCart();
            }
            return RedirectToAction("ShoppingCart");

        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult AddToCart(int Id, bool goToCart)
        {

            List<ProductModel> productList;
            string name = HttpContext.GetUsername();
            _service.AddToCart(Id, name);
            if (string.IsNullOrEmpty(name))
            {
                var product = _service.GetProductById(Id);
                List<int> cartListIds = HttpContext.AddProductToCart(product);
                productList = GetListFromIds(cartListIds);
            }
            else productList = _service.GetCart(name).ToList();
            return (goToCart == true) ? View("Views/Home/ShoppingCart.cshtml", productList) : RedirectToAction("Index") as IActionResult;


        }

        public IActionResult ProductDetails(int Id)
        {

            if (Id == default) RedirectToAction("Index");
            return View("Views/Home/ProductDetails.cshtml", _service.GetProductById(Id));


        }

        public IActionResult ShoppingCart()
        {

            var name = HttpContext.GetUsername();
            List<ProductModel> productList;
            if (!string.IsNullOrEmpty(name))
                productList = _service.GetCart(name).ToList();
            else
            {
                List<int> idList = HttpContext.GetProductsInCart();
                productList = GetListFromIds(idList ?? new List<int>());
            }
            return View("Views/Home/ShoppingCart.cshtml", productList ?? new List<ProductModel>());

        }

        public IActionResult SortByTitle()
        {
            return View("Index", _service.GetAllOrderedByTitleAsync().Result.ToList());

        }

        public IActionResult SortByDate()
        {
            return View("Index", _service.GetAllOrderedByDateAsync().Result.ToList());

        }

        public IActionResult AddProduct()
        {
            if (!HttpContext.CheckIfUserLogin()) return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductModel model)
        {

            if (ModelState.IsValid)
            {
                model.Date = DateTime.Now;
                model.Img1 = model.Img1File.GetImageFromFile();
                model.Img2 = model.Img2File.GetImageFromFile();
                model.Img3 = model.Img3File.GetImageFromFile();
                model.LastModified = DateTime.Now;
                _service.AddProduct(model, HttpContext.GetUsername());
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult RemoveFromCart(int productId)
        {

            _service.RemoveFromCart(productId);
            if (!HttpContext.CheckIfUserLogin())
                HttpContext.RemoveProductFromCart(productId);
            return RedirectToAction("ShoppingCart");

        }

        private List<ProductModel> GetListFromIds(List<int> idList)
        {
            var list = new List<ProductModel>();
            foreach (var id in idList)
            {
                var product = _service.GetProductById(id);
                if (product != null && product.State == State.InCart) list.Add(product);
            }
            return list;
        }

        private async Task CheckLastModified()
        {

            if (!HttpContext.CheckIfUserLogin())
            {
                // אם יש אקספשן לעשות RESULT במקום AWAIT
                var list = _service.CheckLastModifiedGuestAsync(HttpContext.GetProductsInCart()).Result;
                HttpContext.RemoveProductsFromCart(list);
            }
            await _service.CheckLastModifiedAsync();
            HttpContext.Session.SetInt32("IsChecked", 1);

        }

       
    }
}

