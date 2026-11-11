using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string UserId = "ali";   // abhi hardcoded, login baad mein

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ORDER HISTORY: /Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .Where(o => o.UserId == UserId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // CHECKOUT: cart page ke button se
        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == UserId)
                .ToListAsync();

            if (cartItems.Count == 0)
            {
                TempData["Error"] = "Cart khali hai.";
                return RedirectToAction("Index", "Cart");
            }

            if (cartItems.Any(c => c.Product.StockQuantity < c.Quantity))
            {
                TempData["Error"] = "Kuch products ka stock kam hai.";
                return RedirectToAction("Index", "Cart");
            }

            var order = new Order
            {
                UserId = UserId,
                Status = "Pending",
                TotalAmount = cartItems.Sum(c => c.Product.Price * c.Quantity),
                Items = cartItems.Select(c => new OrderItem
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    UnitPrice = c.Product.Price      // price snapshot
                }).ToList()
            };
            _context.Orders.Add(order);

            foreach (var item in cartItems)
            {
                item.Product.StockQuantity -= item.Quantity;
            }

            _context.Carts.RemoveRange(cartItems);

            await _context.SaveChangesAsync();       // sirf ek baar

            TempData["Success"] = "Order place ho gaya!";
            return RedirectToAction("Index");
        }
    }
}