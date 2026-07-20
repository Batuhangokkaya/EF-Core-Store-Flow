using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents
{
    public class _CardStatisticsDashboardComponentPartial : ViewComponent
    {
        private readonly StoreContext _context;

        public _CardStatisticsDashboardComponentPartial(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.TotalCustomerCount   = _context.Customers.Count();
            ViewBag.TotalCategoryCount   = _context.Categories.Count();
            ViewBag.TotalProductCount    = _context.Products.Count();
            ViewBag.AVGCustomerBalance   = _context.Customers.Average(x => x.Balance);
            ViewBag.TotalOrderCount      = _context.Orders.Count();
            ViewBag.SUMOrderProductCount = _context.Orders.Sum(x => x.OrderCount);
            return View();
        }
    }
}