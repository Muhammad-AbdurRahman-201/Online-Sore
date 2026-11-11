
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;


namespace OnlineStore.Controllers
{


    public class ProductsController : Controller
    {

        public ProductsController(ApplicationDbContext context) => _context = context;
        private readonly ApplicationDbContext _context;


        public async Task<IActionResult> Index()
        {
         var data= await _context.Products
                .AsNoTracking()
                .Where(p => p.DeletedAt == null)
                .ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && p.DeletedAt == null);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


    }
}