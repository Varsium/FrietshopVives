using FrietshopVives.Models;
using FrietshopVives.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrietshopVives.Controllers
{

    public class CheckoutController : Controller
    {
        private readonly IOrderCartService _orderCartService;
        public CheckoutController(IOrderCartService iOrderOrderCartService)
        {
            _orderCartService = iOrderOrderCartService;
        }

        public IActionResult Index()
        {
            var orderCart = new OrderCart
            {
                ProductsInCart = _orderCartService.GetProducts(),
                TotalPrice = _orderCartService.GetTotalPrice()
            };
            return View(orderCart);
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

        public IActionResult Pay()
        {
            if (!_orderCartService.RemoveAllProductsFromCart())
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}