using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Products
                .AsNoTracking()
                .Where(p => p.DeletedAt == null)
                .ToListAsync();

            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.DeletedAt == null);
            return data == null ? NotFound() : Ok(data);
         
        }
    }
}
