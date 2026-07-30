using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents.DashboardChartsComponents
{
    public class _DashboardOrderStatusChartComponentPartial : ViewComponent
    {
        private readonly StoreContext _context;

        public _DashboardOrderStatusChartComponentPartial(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Orders
                .GroupBy(x => x.Status)
                .Select(x => new OrderStatusChartViewModel
                {
                    Status = x.Key,
                    Count  = x.Count()
                })
                .ToList();

            return View(values);
        }
    }
}