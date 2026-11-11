using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string UserId = "ali";   // abhi hardcoded, login baad mein

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. CART PAGE: /Cart
        public async Task<IActionResult> Index()
        {
            var items = await _context.Carts
                .AsNoTracking()
                .Include(c => c.Product)
                .Where(c => c.UserId == UserId)
                .ToListAsync();

            return View(items);
        }

        // 2. ADD: Products/Details page ke button se
        [HttpPost]
        public async Task<IActionResult> Add(int productId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId && p.DeletedAt == null);
            if (product == null)
                return NotFound();

            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == UserId && c.ProductId == productId);
            if (cartItem != null)
            {
                if (cartItem.Quantity + 1 <= product.StockQuantity)
                    cartItem.Quantity++;
                else
                    TempData["Error"] = $"{product.Title} ka itna stock nahi hai. Sirf {product.StockQuantity} available hain.";
            }
            else if (product.StockQuantity >= 1)
            {
                _context.Carts.Add(new Cart
                {
                    UserId = UserId,
                    ProductId = productId,
                    Quantity = 1
                });
            }
            else
            {
                TempData["Error"] = $"{product.Title} out of stock hai.";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // 3. UPDATE QUANTITY: cart page ke + / - buttons se
        [HttpPost]
        public async Task<IActionResult> Update(int productId, int quantity)
        {
            var cartItem = await _context.Carts
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.UserId == UserId && c.ProductId == productId);
            if (cartItem == null)
                return NotFound();

            if (quantity < 1)
            {
                _context.Carts.Remove(cartItem);           // 0 ho gayi to row hata do
            }
            else if (quantity <= cartItem.Product.StockQuantity)
            {
                cartItem.Quantity = quantity;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // 4. REMOVE: cart page ke Remove button se
        [HttpPost]
        public async Task<IActionResult> Remove(int productId)
        {
            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == UserId && c.ProductId == productId);
            if (cartItem == null)
                return NotFound();

            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}