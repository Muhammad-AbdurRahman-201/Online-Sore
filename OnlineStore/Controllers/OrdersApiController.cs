using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(CheckoutRequest request)
        {
            // 1. Cart uthao
            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == request.UserId)
                .ToListAsync();

            if (cartItems.Count == 0)
                return BadRequest("Cart khali hai");

            // 2. Har item ka stock check
            foreach (var item in cartItems)
            {
                if (item.Quantity > item.Product.StockQuantity)
                    return BadRequest($"{item.Product.Title} ka stock kam hai");
            }

            // 3. Order banao
            var order = new Order
            {
                UserId = request.UserId,
                Status = "Pending"
            };

            // 4. Har cart row se OrderItem banao aur stock kam karo
            foreach (var item in cartItems)
            {
                order.Items.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price      // price snapshot
                });

                item.Product.StockQuantity -= item.Quantity;
            }

            // 5. Total nikalo
            order.TotalAmount = order.Items.Sum(i => i.UnitPrice * i.Quantity);

            // 6. Order daalo aur cart khali karo
            _context.Orders.Add(order);
            _context.Carts.RemoveRange(cartItems);

            // 7. Sab kuch ek hi baar save
            await _context.SaveChangesAsync();

            return Ok(new { orderId = order.Id, total = order.TotalAmount });
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrders(string userId)
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new
                {
                    o.Id,
                    o.OrderDate,
                    o.Status,
                    o.TotalAmount,
                    Items = o.Items.Select(i => new
                    {
                        i.ProductId,
                        i.Product.Title,
                        i.UnitPrice,
                        i.Quantity
                    })
                })
                .ToListAsync();

            return Ok(orders);
        }
    }
}