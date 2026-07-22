using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APEX_WMS.Data;
using APEX_WMS.Models;

namespace APEX_WMS.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.Supplier)
                .Include(o => o.OrderDetails)
                .ToListAsync();
            return View(orders);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var order = await _context.Orders
                .Include(o => o.Supplier)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.OrderTypes = new SelectList(Enum.GetValues(typeof(OrderType))
                .Cast<OrderType>()
                .Select(x => new { Id = (int)x, Name = x.ToString() }),
                "Id", "Name");

            ViewBag.Suppliers = new SelectList(
                await _context.Suppliers.ToListAsync(),
                "SupplierId", "SupplierName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderNumber,OrderType,SupplierId,OrderDate,TotalAmount,Notes")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Status = OrderStatus.Draft;
                order.CreatedDate = DateTime.Now;
                order.ModifiedDate = DateTime.Now;

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = order.OrderId });
            }

            ViewBag.OrderTypes = new SelectList(Enum.GetValues(typeof(OrderType))
                .Cast<OrderType>()
                .Select(x => new { Id = (int)x, Name = x.ToString() }),
                "Id", "Name");

            ViewBag.Suppliers = new SelectList(
                await _context.Suppliers.ToListAsync(),
                "SupplierId", "SupplierName", order.SupplierId);

            return View(order);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
