using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.LayoutComponents
{
    public class _LayoutFooterDashboardComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}