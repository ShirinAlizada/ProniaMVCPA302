using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaMVCPA302.DAL;
using ProniaMVCPA302.Models;
using ProniaMVCPA302.Utilities.Enums;
using ProniaMVCPA302.Utilities.Extensions;
using ProniaMVCPA302.ViewModels;


namespace ProniaMVCPA302.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlideController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            List<SlideItemVM> slideVMs = await _context.Slides.Select(s => new SlideItemVM
            {
                Id = s.Id,
                Title = s.Title,
                Order = s.Order,
                CreatedAt = DateTime.Now,
                Image = s.Image
            }).ToListAsync();





            return View(slideVMs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSlideVM slideVM)
        {
            if (!ModelState.IsValid) return View(slideVM);
            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Order == slideVM.Order);
            if (existed is not null)
            {
                ModelState.AddModelError(nameof(CreateSlideVM.Order), $"Order: {slideVM.Order} already exists.");
                return View(slideVM);
            }
            if (!slideVM.Photo.ValidateSize(FileSize.MB, 2))
            {
                ModelState.AddModelError(nameof(CreateSlideVM.Photo), "Image size must be less than 2MB.");
                return View(slideVM);
            }

            if (!slideVM.Photo.ValidateType("Image"))
            {
                ModelState.AddModelError(nameof(CreateSlideVM.Photo), "File type is incorrect");
                return View(slideVM);
            }



            bool result = await _context.Slides.AnyAsync(s => s.Order == slideVM.Order);
            if (result)
            {
                ModelState.AddModelError(nameof(CreateSlideVM.Title), $"Title {slideVM.Title} already exists.");
                return View(slideVM);
            }

            string image = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");

            Slide slide = new Slide
            {
                Title = slideVM.Title,
                SubTitle = slideVM.SubTitle,
                Description = slideVM.Description,
                Order = slideVM.Order,
                Image = image,
                CreatedAt = DateTime.Now
            };
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
            if (id is null || id < 1)  return BadRequest();
            
            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            
            if (existed is null) return NotFound();

            UpdateSlideVm slideVm = new()
            {
                Title = existed.Title,
                SubTitle = existed.SubTitle,
                Description = existed.Description,
                Image = existed.Image,
                Order = existed.Order
            };

            return View(slideVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateSlideVm slideVM)
        {

            
            if (!ModelState.IsValid) return View(slideVM);

            if(slideVM.Photo is not null)
            {
                if(!slideVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(UpdateSlideVm.Photo), "Image size must be less than 2MB.");
                    return View(slideVM);
                }
                if (!slideVM.Photo.ValidateType("Image"))
                {
                    ModelState.AddModelError(nameof(UpdateSlideVm.Photo), "File type is Invalid");
                    return View(slideVM);
                }


            }


            bool result = await _context.Slides.AnyAsync(s => s.Order == slideVM.Order && s.Id != id);
            if (result)
            {
                ModelState.AddModelError(nameof(UpdateSlideVm.Order), $"Order {slideVM.Order} already exists.");
                return View(slideVM);
            }

            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (slideVM.Photo is not null)
            {
                string newFileName = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
                existed.Image = newFileName;
            }

            

            existed.Title = slideVM.Title;
            existed.SubTitle = slideVM.SubTitle;
            existed.Order = slideVM.Order;
            existed.Description = slideVM.Description;
            


            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Slide? existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            
            if (existed is null) return NotFound();
            
            existed.Image.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");


            _context.Slides.Remove(existed);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));

        }
    }
}