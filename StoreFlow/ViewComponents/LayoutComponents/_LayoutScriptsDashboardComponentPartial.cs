using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.LayoutComponents
{
    public class _LayoutScriptsDashboardComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}