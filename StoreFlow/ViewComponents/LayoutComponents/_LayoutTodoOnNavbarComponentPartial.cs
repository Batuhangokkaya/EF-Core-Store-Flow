using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.LayoutComponents
{
    public class _LayoutTodoOnNavbarComponentPartial : ViewComponent
    {
        private readonly StoreContext _context;

        public _LayoutTodoOnNavbarComponentPartial(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Todos
                .Where(x => x.Status == false)
                .OrderByDescending(x => x.TodoID)
                .Take(5)
                .ToList();
            ViewBag.TodoTotalCount = _context.Todos.Count();
            return View(values);
        }
    }
}