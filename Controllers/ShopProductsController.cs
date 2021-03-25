using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrietshopVives.Models;
using FrietshopVives.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrietshopVives.Controllers
{
    public class ShopProductsController : Controller
    {
        private readonly IProductService _productService;

        public ShopProductsController(IProductService iProductService)
        {
            _productService = iProductService;
        }

        public IActionResult Index()
        {

            return View(_productService.GetProducts());

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            _productService.Create(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            if (_productService.Get(id) == null)
            {
                return RedirectToAction("Index");
            }

            return View(_productService.Get(id));
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            _productService.Update(product);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (!_productService.Delete(id))
            {
                return RedirectToAction("ErrorPage");
            }

            return RedirectToAction("Index");
        }

        public IActionResult ErrorPage()
        {
            return View();
        }


    }
}