using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents
{
    public class _DailySalesDashboardComponentPartial : ViewComponent
    {
        private readonly StoreContext _context;

        public _DailySalesDashboardComponentPartial(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Todos
                .GroupBy(x => x.Priority)
                .Select(x => new TodoStatusChartViewModel
                {
                    Priority = x.Key,
                    Count    = x.Count()
                })
                .ToList();

            return View(values);
        }
    }
}