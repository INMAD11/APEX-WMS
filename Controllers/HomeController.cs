using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APEX_WMS.Data;
using System.Diagnostics;

namespace APEX_WMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new Dictionary<string, object>
            {
                { "TotalProducts", await _context.Products.CountAsync() },
                { "TotalSuppliers", await _context.Suppliers.CountAsync() },
                { "TotalInventory", await _context.Inventories.CountAsync() },
                { "PendingOrders", await _context.Orders.CountAsync(o => o.Status == Models.OrderStatus.Pending) },
                { "LowStockItems", await _context.Inventories.CountAsync(i => i.AvailableQuantity <= 10) }
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
