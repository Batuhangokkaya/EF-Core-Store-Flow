using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Entities;

namespace StoreFlow.Controllers
{
    public class ActivityController : Controller
    {
        private readonly StoreContext _context;

        public ActivityController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.Activities.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Activity activity)
        {
            _context.Activities.Add(activity);
            _context.SaveChanges();
            return RedirectToAction("Index", "Activity");
        }

        public IActionResult Delete(int id)
        {
            var value = _context.Activities.Find(id);
            _context.Activities.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "Activity");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var value = _context.Activities.Find(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult Update(Activity activity)
        {
            _context.Activities.Update(activity);
            _context.SaveChanges();
            return RedirectToAction("Index", "Activity");
        }
    }
}