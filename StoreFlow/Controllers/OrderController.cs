using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.Controllers
{
    public class OrderController : Controller
    {
        private readonly StoreContext _context;

        public OrderController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult AllStockSmallerThen5()
        {
            bool orderStockCount = _context.Orders.All(x => x.OrderCount <= 5);

            if (orderStockCount)
            {
                ViewBag.OrderStockCount = "Tüm siparişler 5 adetten küçüktür.";
            }
            else
            {
                ViewBag.OrderStockCount = "Tüm siparişler 5 adetten küçük değildir.";
            }

            return View();
        }

        public IActionResult OrderListByStatus(string status)
        {
            var values = _context.Orders
                .Where(x => x.Status.Contains(status))
                .ToList();

            if (!values.Any())
            {
                ViewBag.Values = "Bu status ile ilgili veri bulunamadı!";
            }

            return View(values);
        }

        public IActionResult OrderListSearch(string name, string filterType)
        {
            if (filterType == "Start")
            {
                var values = _context.Orders
                    .Where(x => x.Status.StartsWith(name))
                    .ToList();

                return View(values);
            }
            else if (filterType == "End")
            {
                var values = _context.Orders
                    .Where(x => x.Status.EndsWith(name))
                    .ToList();

                return View(values);
            }

            var orderValues = _context.Orders.ToList();
            return View(orderValues);
        }
    }
}
