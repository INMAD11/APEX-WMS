using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APEX_WMS.Data;
using APEX_WMS.Models;

namespace APEX_WMS.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InventoryController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var inventory = await _context.Inventories
                .Include(i => i.Product)
                .ToListAsync();
            return View(inventory);
        }

        public async Task<IActionResult> StockIn()
        {
            var products = await _context.Products.ToListAsync();
            ViewBag.Products = products;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StockIn(int productId, int quantity, string location, string remarks)
        {
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.ProductId == productId && i.Location == location);

            if (inventory == null)
            {
                inventory = new Inventory
                {
                    ProductId = productId,
                    Location = location,
                    QuantityOnHand = quantity
                };
                _context.Inventories.Add(inventory);
            }
            else
            {
                inventory.QuantityOnHand += quantity;
            }

            inventory.LastStockCheckDate = DateTime.Now;
            inventory.ModifiedDate = DateTime.Now;

            var movement = new StockMovement
            {
                ProductId = productId,
                MovementType = MovementType.StockIn,
                Quantity = quantity,
                ToLocation = location,
                Remarks = remarks,
                UserId = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
                MovementDate = DateTime.Now,
                CreatedDate = DateTime.Now
            };

            _context.StockMovements.Add(movement);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Stock added successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> StockOut()
        {
            var inventory = await _context.Inventories
                .Include(i => i.Product)
                .ToListAsync();
            return View(inventory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StockOut(int inventoryId, int quantity, string remarks)
        {
            var inventory = await _context.Inventories.FindAsync(inventoryId);

            if (inventory == null || quantity > inventory.AvailableQuantity)
            {
                TempData["Error"] = "Insufficient stock available";
                return RedirectToAction(nameof(StockOut));
            }

            inventory.QuantityOnHand -= quantity;
            inventory.ModifiedDate = DateTime.Now;

            var movement = new StockMovement
            {
                ProductId = inventory.ProductId,
                MovementType = MovementType.StockOut,
                Quantity = quantity,
                FromLocation = inventory.Location,
                Remarks = remarks,
                UserId = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
                MovementDate = DateTime.Now,
                CreatedDate = DateTime.Now
            };

            _context.StockMovements.Add(movement);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Stock removed successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
