using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;

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
