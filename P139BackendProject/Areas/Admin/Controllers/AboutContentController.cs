using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using P139BackendProject.Areas.Admin.ViewModels.AboutContent;
using P139BackendProject.Areas.Admin.ViewModels.Contact;
using P139BackendProject.Data;
using P139BackendProject.Services;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutContentController : Controller
    {
        private readonly IAboutContentService _aboutContentService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AboutContentController(IAboutContentService aboutContentService, AppDbContext context, IMapper mapper)
        {
            _aboutContentService = aboutContentService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _aboutContentService.GetDataAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            AboutContentVM aboutContent = await _aboutContentService.GetByIdAsync((int)id);

            if (aboutContent is null) return NotFound();

            return View(aboutContent);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            AboutContentVM dbAboutContent = await _aboutContentService.GetByIdAsync((int)id);

            if (dbAboutContent is null) return NotFound();

            AboutContentEditVM model = _mapper.Map<AboutContentEditVM>(dbAboutContent);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AboutContentEditVM request)
        {
            if (id is null) return BadRequest();

            AboutContentVM dbAboutContent = await _aboutContentService.GetByIdAsync((int)id);

            if (dbAboutContent is null) return NotFound();


            if (!ModelState.IsValid)
            {
                return View();
            }

            await _aboutContentService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
