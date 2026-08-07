using Microsoft.AspNetCore.Mvc;
using ProniaMVCPA302.DAL;
using ProniaMVCPA302.Models;
using Microsoft.EntityFrameworkCore;

namespace ProniaMVCPA302.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController : Controller
    {
        private readonly AppDbContext _context;

        public SlideController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Slide> slides = await _context.Slides.ToListAsync();


            return View(slides);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
