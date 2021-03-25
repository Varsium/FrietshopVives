using FrietshopVives.Models;
using FrietshopVives.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrietshopVives.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderCartService _orderCartService;
        public HomeController(IProductService iProductService, IOrderCartService iOrderCartService)
        {
            _productService = iProductService;
            _orderCartService = iOrderCartService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var orderCart = new OrderCart
            {
                ProductsInCart = _orderCartService.GetProducts(),
                TotalPrice = _orderCartService.GetTotalPrice()
            };
            var shopModel = new ShopModel
            {

                Products = _productService.GetProducts(),

                OrderCart = orderCart

            };
            return View(shopModel);
        }

        [HttpPost]
        public IActionResult PutProductInCart(int id)
        {

            var originalProduct = _productService.Get(id);
            if (originalProduct == null)
            {
                return RedirectToAction("Index");
            }

            var itemToCart = new Product
            {
                Quantity = originalProduct.Quantity,
                Id = originalProduct.Id,
                Ingredient = originalProduct.Ingredient,
                Price = originalProduct.Price
            };
            _orderCartService.AddProductToCart(itemToCart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult TakeProductOutCart(int id)
        {
            if (_orderCartService.Get(id) == null)
            {
                return RedirectToAction("Index");
            }
            _orderCartService.RemoveProductFromCart(id);
            return RedirectToAction("Index");
        }

    }
}
