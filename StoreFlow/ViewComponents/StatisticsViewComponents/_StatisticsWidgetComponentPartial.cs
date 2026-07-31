using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.StatisticsViewComponents
{
    public class _StatisticsWidgetComponentPartial : ViewComponent
    {
        private readonly StoreContext _context;

        public _StatisticsWidgetComponentPartial(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.CategoryCount                               = _context.Categories.Count();
            ViewBag.ProductMaxPrice                             = _context.Products.Max(x => x.Price);
            ViewBag.ProductMinPrice                             = _context.Products.Min(x => x.Price);
            ViewBag.ProductMaxPriceProductName                  = _context.Products.Where(x => x.Price == (_context.Products.Max(x => x.Price))).Select(x => x.Name).FirstOrDefault();
            ViewBag.ProductMinPriceProductName                  = _context.Products.Where(x => x.Price == (_context.Products.Min(x => x.Price))).Select(x => x.Name).FirstOrDefault();

            ViewBag.TotalSumProductStock                        = _context.Products.Sum(x => x.Stock);
            ViewBag.AverageProductStock                         = _context.Products.Average(x => x.Stock);
            ViewBag.AverageProductPrice                         = _context.Products.Average(x => x.Price);

            ViewBag.BiggerPriceThen1000ProductCount             = _context.Products.Where(x => x.Price >= 1000).Count();
            ViewBag.GetIDIs4ProductName                         = _context.Products.Where(x => x.ProductID == 4).Select(x => x.Name).FirstOrDefault();
            ViewBag.StockCountBigger50AndSmaller100ProductCount = _context.Products.Where(x => x.Stock >= 50 && x.Stock <= 100).Count();

            ViewBag.CustomerCount                               = _context.Customers.Count();
            ViewBag.OrderCount                                  = _context.Orders.Count();
            var highestBalanceCustomer                          = _context.Customers.OrderByDescending(x => x.Balance).FirstOrDefault();
            ViewBag.HighestBalanceCustomer                      = highestBalanceCustomer == null ? "Müşteri Bulunamadı" : highestBalanceCustomer.Name + " " + highestBalanceCustomer.Surname;
            ViewBag.HighestBalance                              = highestBalanceCustomer.Balance;

            return View();
        }
    }
}