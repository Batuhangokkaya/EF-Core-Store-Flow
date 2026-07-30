using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents
{
    public class _SalesStatusDashboardComponentPartial : ViewComponent
    {
        private readonly StoreContext _context;

        public _SalesStatusDashboardComponentPartial(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Customers
                .GroupBy(x => x.City)
                .Select(x => new CustomerCityChartViewModel
                {
                    City  = x.Key,
                    Count = x.Count()
                })
                .ToList();

            return View(values);
        }
    }
}