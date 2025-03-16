using Microsoft.AspNetCore.Mvc;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessOrder(decimal orderAmount, string customerType)
        {
            if (orderAmount <= 0 || string.IsNullOrEmpty(customerType))
            {
                ModelState.AddModelError("", "Invalid order amount or customer type.");
                return View("Index");
            }

            decimal discount = 0;
            if (orderAmount >= 100 && customerType == "Loyal")
            {
                discount = orderAmount * 0.10m; // 10% discount
            }

            decimal finalAmount = orderAmount - discount;

            var order = new Order
            {
                OrderAmount = orderAmount,
                CustomerType = customerType,
                Discount = discount,
                FinalAmount = finalAmount
            };

            return View("Result", order);
        }
    }
}
