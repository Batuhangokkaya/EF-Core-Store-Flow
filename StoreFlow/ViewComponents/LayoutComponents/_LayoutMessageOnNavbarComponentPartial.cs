using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.LayoutComponents
{
    public class _LayoutMessageOnNavbarComponentPartial : ViewComponent
    {
        private readonly StoreContext _context;

        public _LayoutMessageOnNavbarComponentPartial(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Messages
                .Where(x => x.IsRead == false)
                .OrderByDescending(x => x.MessageID)
                .Take(5)
                .ToList();
            ViewBag.MessageCount = _context.Messages.Count();
            return View(values);
        }
    }
}