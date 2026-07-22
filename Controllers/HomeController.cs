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
            var totalProducts = await _context.Products.CountAsync();
            var totalSuppliers = await _context.Suppliers.CountAsync();
            var totalInventory = await _context.Inventories.CountAsync();
            var pendingOrders = await _context.Orders.CountAsync(o => o.Status == Models.OrderStatus.Pending);
            
            // Fetch inventories and calculate low stock items client-side
            var inventories = await _context.Inventories.ToListAsync();
            var lowStockItems = inventories.Count(i => i.AvailableQuantity <= 10);

            var viewModel = new Dictionary<string, object>
            {
                { "TotalProducts", totalProducts },
                { "TotalSuppliers", totalSuppliers },
                { "TotalInventory", totalInventory },
                { "PendingOrders", pendingOrders },
                { "LowStockItems", lowStockItems }
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
