using Microsoft.AspNetCore.Mvc;
using ProniaMVCPA302.DAL;
using ProniaMVCPA302.Models;
using ProniaMVCPA302.ViewModels;

namespace ProniaMVCPA302.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController()
        {
            _context = new AppDbContext();

        }
        public IActionResult Index()
        {
            List<Slide> slides = _context.Sliders
                .OrderBy(s => s.Order)
                .Take(2)
                .ToList();


            HomeVM homeVM = new HomeVM
            {
                Slides = slides
            };
            return View(homeVM);
        }
    }
}
