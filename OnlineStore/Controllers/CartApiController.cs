using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CartRequest request)
        {
            // 1. Quantity check
            if (request.Quantity < 1)
                return BadRequest("Quantity kam az kam 1 honi chahiye");

            // 2. Product maujood hai?
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.ProductId && p.DeletedAt == null);

            if (product == null)
                return NotFound("Product nahi mila");

            // 3. Purani row dhoondo
            var existingCartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == request.UserId && c.ProductId == request.ProductId);

            if (existingCartItem != null)
            {
                // 4a. Row mil gayi: stock check, phir quantity badhao
                if (existingCartItem.Quantity + request.Quantity > product.StockQuantity)
                    return BadRequest("Itna stock maujood nahi hai");

                existingCartItem.Quantity += request.Quantity;
            }
            else
            {
                // 4b. Row nahi mili: stock check, phir nayi row banao
                if (request.Quantity > product.StockQuantity)
                    return BadRequest("Itna stock maujood nahi hai");

                var cartItem = new Cart
                {
                    UserId = request.UserId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };
                _context.Carts.Add(cartItem);
            }

            // 5. Aakhir mein ek hi baar save
            await _context.SaveChangesAsync();
            return Ok("Item added to cart");
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
         
            var items = await _context.Carts.AsNoTracking()
                .Where(c => c.UserId == userId)
                .Include(c => c.Product) 
                .ToListAsync();
            var todosum = items.Sum(i => i.Quantity * i.Product.Price);
            return Ok(new { Items = items, TotalSum = todosum });
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(CartRequest request)
        {
       
            var find=await _context.Carts.FirstOrDefaultAsync(c => c.UserId == request.UserId && c.ProductId == request.ProductId);

            if (find == null)
                return NotFound("Item not found in cart");
    
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId && p.DeletedAt == null);
            if (product == null)
                return NotFound("Product not found");
            if (request.Quantity > product.StockQuantity)
                return BadRequest("Insufficient stock available");

            find.Quantity = request.Quantity;

            await _context.SaveChangesAsync();
            return Ok("Cart item updated successfully");
        }

        [HttpDelete("remove/{userId}/{productId}")]
        public async Task<IActionResult> Remove(string userId, int productId)
        {
            var del = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
        
            if (del == null)
                return NotFound("Item not found in cart");

            _context.Carts.Remove(del);
     
            await _context.SaveChangesAsync();
            return Ok("Item removed from cart");        
        }
    }
}