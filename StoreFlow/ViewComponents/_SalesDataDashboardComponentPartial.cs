using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents
{
    public class _SalesDataDashboardComponentPartial : ViewComponent
    {
        private readonly StoreContext _context;

        public _SalesDataDashboardComponentPartial(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Orders
                .OrderByDescending(x => x.OrderID)
                .Include(x => x.Customer)
                .Include(x => x.Product)
                .Take(5)
                .ToList();
            return View(values);
        }
    }
}