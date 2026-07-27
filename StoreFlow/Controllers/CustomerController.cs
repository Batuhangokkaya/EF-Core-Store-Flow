using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models;

namespace StoreFlow.Controllers
{
    public class CustomerController : Controller
    {
        private readonly StoreContext _context;

        public CustomerController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult CustomerListOrderByCustomerName()
        {
            var values = _context.Customers
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Surname)
                .ToList();
            return View(values);
        }

        public IActionResult CustomerListOrderByDescendingBalance()
        {
            var values = _context.Customers
                .OrderByDescending(x => x.Balance)
                .ToList();
            return View(values);
        }

        public IActionResult CustomerGetByCity(string city)
        {
            var exist = _context.Customers
                .Any(x => x.City == city);
            
            if (exist)
            {
                ViewBag.Message = $"{city} şehrinde en az 1 tane müşteri var.";
            }
            else
            {
                ViewBag.Message = $"{city} şehrinde hiç müşteri yok.";
            }
            
            return View();
        }

        public IActionResult CustomerListByCity()
        {
            var groupedCustomers = _context.Customers
                .GroupBy(x => x.City)
                .ToList();

            return View(groupedCustomers);
        }

        public IActionResult CustomersByCityCount()
        {
            var query =
                from c in _context.Customers
                group c by c.City into cityGroup
                select new CustomerCityGroup
                {
                    City = cityGroup.Key,
                    CustomerCount = cityGroup.Count()
                };

            var model = query.OrderByDescending(x => x.CustomerCount).ToList();

            return View(model);
        }

        public IActionResult CustomersCityList()
        {
            var values = _context.Customers
                .Select(x => x.City)
                .Distinct()
                .ToList();
            return View(values);
        }

        public IActionResult ParallelCustomers()
        {
            var customers = _context.Customers.ToList();
            var result    = customers
                .AsParallel()
                .Where(x => x.City.StartsWith("K", StringComparison.OrdinalIgnoreCase))
                .ToList();
            return View(result);
        }

        public IActionResult CustomerListExceptCityKahramanmaras()
        {
            // Except
            /*
            var customers = _context.Customers.ToList();
            var customersListInKahramanmaras = _context.Customers
                .Where(c => c.City == "Istanbul")
                .ToList();

            var result = customers.Except(customersListInKahramanmaras).ToList();
            */

            var customers                    = _context.Customers.ToList();
            var customersListInKahramanmaras = _context.Customers
                .Where(x => x.City == "Kahramanmaraş")
                .Select(x => x.CustomerID)
                .ToList();

            var result = customers.ExceptBy(customersListInKahramanmaras, x => x.CustomerID).ToList();

            return View(result);
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            var categories = _context.Categories
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.CategoryID.ToString()
                })
                .ToList();
            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("CustomerList", "Customer");
        }

        public IActionResult DeleteCustomer(int id)
        {
            var value = _context.Customers.Find(id);
            _context.Customers.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("CustomerList", "Customer");
        }

        [HttpGet]
        public IActionResult UpdateCustomer(int id)
        {
            var categories = _context.Categories
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.CategoryID.ToString()
                })
                .ToList();
            ViewBag.Categories = categories;

            var value = _context.Customers.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return RedirectToAction("CustomerList", "Customer");
        }
    }
}
