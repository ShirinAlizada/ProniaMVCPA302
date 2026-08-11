using Microsoft.AspNetCore.Mvc;
using ProniaMVCPA302.DAL;
using ProniaMVCPA302.Models;
using Microsoft.EntityFrameworkCore;

namespace ProniaMVCPA302.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages.OrderByDescending(pi => pi.IsPrimary))
                .ToListAsync();

            return View(products);
        }
    }
}
