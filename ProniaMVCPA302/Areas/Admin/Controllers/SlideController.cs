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
        [HttpPost]
        public async Task<IActionResult> Create(Slide slide)
        {
            if (!ModelState.IsValid) return View(slide);
            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Order == slide.Order);
            if (existed is not null)
            {
                ModelState.AddModelError("Order", "Slide with this order already exists.");
                return View(slide);
            }
            bool result = await _context.Slides.AnyAsync(s => s.Order == slide.Order);
            if (result)
            {
                ModelState.AddModelError("Title", "Slide with this title already exists.");
                return View(slide);
            }
            slide.CreatedAt = DateTime.Now;
            _context.Slides.Add(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Slide? slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (slide is null) return NotFound();

            return View(slide);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id < 1)
                return BadRequest();
            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (existed is null) return NotFound();
            return View(existed);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Slide slide)
        {
            if (!ModelState.IsValid) return View();
            bool result = await _context.Slides.AnyAsync(s => s.Order == slide.Order && s.Id != id);
            if (result)
            {
                ModelState.AddModelError("Order", "Slide with this order already exists.");
                return View(slide);
            }
            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            existed.Title = slide.Title;
            existed.SubTitle = slide.SubTitle;
            existed.Order = slide.Order;
            existed.Description = slide.Description;
            existed.Image = slide.Image;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1) return BadRequest();
            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (existed is null) return NotFound();
            _context.Slides.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}