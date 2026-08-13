using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaMVCPA302.DAL;
using ProniaMVCPA302.Models;
using ProniaMVCPA302.Services;
using ProniaMVCPA302.ViewModels;

namespace ProniaMVCPA302.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        
        public HomeController(AppDbContext context)
        {
            _context = context;
            
        }
        public async Task<IActionResult> Index()
        {
            
            List<Slide> slides = await _context.Slides
                .OrderBy(s => s.Order)
                .Take(2)
                .ToListAsync();

            List<ProductItemVM> productVMs = await _context.Products
                .Take(8)
                .Select(p => new ProductItemVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    MainImage = p.ProductImages.FirstOrDefault(pi=>pi.IsPrimary==true).Image,
                    SecondaryImage = p.ProductImages.FirstOrDefault(pi => pi.IsPrimary == false).Image
                })
                .ToListAsync();



            HomeVM homeVM = new HomeVM
            {
                Slides = slides,
                Products = productVMs
            };
            return View(homeVM);
        }
    }
}
