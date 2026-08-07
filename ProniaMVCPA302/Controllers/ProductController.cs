using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaMVCPA302.DAL;
using ProniaMVCPA302.Models;
using ProniaMVCPA302.ViewModels;

namespace ProniaMVCPA302.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Product? product = await _context.Products
                .Include(p => p.ProductImages.OrderByDescending(pi => pi.IsPrimary))
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            DetailVM detailVM = new DetailVM
            {
                Product = product,

                RelatedProduct = await _context.Products
                .Take(8)
                .Where(p => p.CategoryId == product.CategoryId && p.Id != id)
                .Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null))
                .ToListAsync()
            }; 

            return View(detailVM);
        }
    }
}
