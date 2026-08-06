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
        private readonly IEmailService _service;
        public HomeController(AppDbContext context, IEmailService service)
        {
            _context = context;
            _service = service;
        }
        public IActionResult Index()
        {
            _service.Send();
            List<Slide> slides = _context.Slides
                .OrderBy(s => s.Order)
                .Take(2)
                .ToList();

            List<Product> products = _context.Products
                .Take(8)
                .Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null))
                .ToList();



            HomeVM homeVM = new HomeVM
            {
                Slides = slides,
                Products = products
            };
            return View(homeVM);
        }
    }
}
