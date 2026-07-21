using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;

namespace StoreFlow.Controllers
{
    public class ProductController : Controller
    {
        private readonly StoreContext _context;

        public ProductController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult ProductList()
        {
            var values = _context.Products
                .Include(x => x.Category)
                .OrderByDescending(x => x.ProductID)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateProduct()
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
        public IActionResult CreateProduct(Product Product)
        {
            _context.Products.Add(Product);
            _context.SaveChanges();
            return RedirectToAction("ProductList", "Product");
        }

        public IActionResult DeleteProduct(int id)
        {
            var value = _context.Products.Find(id);
            _context.Products.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("ProductList", "Product");
        }

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            var categories = _context.Categories
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.CategoryID.ToString()
                })
                .ToList();
            ViewBag.Categories = categories;

            var value = _context.Products.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product Product)
        {
            _context.Products.Update(Product);
            _context.SaveChanges();
            return RedirectToAction("ProductList", "Product");
        }

        public IActionResult First5Product()
        {
            var values = _context.Products
                .Include(x => x.Category)
                .Take(5)
                .ToList();
            return View(values);
        }

        public IActionResult Skip4Product()
        {
            var values = _context.Products
                .Include(x => x.Category)
                .Skip(4)
                .Take(10)
                .ToList();
            return View(values);
        }
    }
}