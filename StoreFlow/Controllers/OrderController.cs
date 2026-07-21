using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;

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

        public async Task<IActionResult> AsyncOrderList()
        {
            var values = await _context.Orders
                .Include(x => x.Product)
                .Include(x => x.Customer)
                .OrderByDescending(x => x.OrderID)
                .ToListAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> AsyncCreateOrder()
        {
            var products = await _context.Products
                .Select(x => new SelectListItem
                {
                    Text  = x.Name,
                    Value = x.ProductID.ToString()
                })
                .ToListAsync();
            ViewBag.Products = products;

            var customers = await _context.Customers
                .Select(x => new SelectListItem
                {
                    Text  = x.Name + " " + x.Surname,
                    Value = x.CustomerID.ToString()
                })
                .ToListAsync();
            ViewBag.Customers = customers;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AsyncCreateOrder(Order order)
        {
            order.Status    = "Sipariş Alındı";
            order.OrderDate = DateTime.Now;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("AsyncOrderList", "Order");
        }

        public async Task<IActionResult> AsyncDeleteOrder(int id)
        {
            var value = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(value);
            await _context.SaveChangesAsync();
            return RedirectToAction("AsyncOrderList", "Order");
        }

        [HttpGet]
        public async Task<IActionResult> AsyncUpdateOrder(int id)
        {
            var products = await _context.Products
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ProductID.ToString()
                })
                .ToListAsync();
            ViewBag.Products = products;

            var customers = await _context.Customers
                .Select(x => new SelectListItem
                {
                    Text = x.Name + " " + x.Surname,
                    Value = x.CustomerID.ToString()
                })
                .ToListAsync();
            ViewBag.Customers = customers;

            var value = await _context.Orders.FindAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> AsyncUpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("AsyncOrderList", "Order");
        }
    }
}