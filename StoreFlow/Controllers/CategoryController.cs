using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Entities;

namespace StoreFlow.Controllers
{
    public class CategoryController : Controller
    {
        private readonly StoreContext _context;

        public CategoryController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult CategoryList()
        {
            var values = _context.Categories.ToList();
            return View(values);
        }

        public IActionResult ReverseCategory()
        {
            var value1 = _context.Categories.First();
            ViewBag.FirstCategory = value1.Name;

            var value2 = _context.Categories.SingleOrDefault(x => x.Name == "Bilgisayar");
            ViewBag.SingleOrDefaultCategory = value2.Status + "-" + value2.CategoryID.ToString();

            var values = _context.Categories
                .OrderBy(x => x.CategoryID)
                .Reverse()
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            category.Status = false;
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("CategoryList", "Category");
        }

        public IActionResult DeleteCategory(int id)
        {
            var value = _context.Categories.Find(id);
            _context.Categories.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("CategoryList", "Category");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _context.Categories.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("CategoryList", "Category");
        }
    }
}