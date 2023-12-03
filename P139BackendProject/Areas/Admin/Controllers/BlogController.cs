using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Blog;
using P139BackendProject.Data;
using P139BackendProject.Helpers;
using P139BackendProject.Helpers.Extentions;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ITagService _tagService;

        public BlogController(IBlogService blogService,
                                 AppDbContext context,
                                 IWebHostEnvironment env,
                                 ITagService tagService)
        {
            _context = context;
            _env = env;
            _blogService = blogService;
            _tagService = tagService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int take = 3)
        {
            List<BlogVM> dbPaginatedDatas = await _blogService.GetPaginatedDatasAsync(page, take);

            int pageCount = await GetPageCountAsync(take);

            Paginate<BlogVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

            return View(paginatedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _blogService.GetCountAsync();
            return (int)Math.Ceiling((decimal)(productCount) / take);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            BlogDetailVM blog = await _blogService.GetByIdAsync((int)id);

            if (blog is null) return NotFound();

            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var tags = _tagService.GetAllSelectedAsync();

            BlogCreateVM viewModel = new BlogCreateVM
            {
                Tags = tags
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(BlogCreateVM request)
        {

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            BlogVM existBlog = await _blogService.GetByNameWithoutTrackingAsync(request.Title);

            if (existBlog is not null)
            {
                ModelState.AddModelError("Title", "This title already exists");

                return View(request);
            }

            foreach (var photo in request.Photos)
            {

                if (!photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photos", "File can be only image format");
                    return View(request);
                }

                if (!photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photos", "File size can be max 200 kb");
                    return View(request);
                }
            }


            await _blogService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            await _blogService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Blog dbBlog = await _context.Blogs.AsNoTracking().Where(m => m.Id == id).Include(m => m.BlogTags).Include(m => m.Images).FirstOrDefaultAsync();

            if (dbBlog is null) return NotFound();

            var selectedTags = dbBlog.BlogTags.Select(m => m.TagId).ToList();

            var tags = _context.Tags.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString(),
                Selected = selectedTags.Contains(m.Id)
            }).ToList();




            return View(new BlogEditVM()
            {
                Title = dbBlog.Title,
                Description = dbBlog.Description,
                Images = dbBlog.Images,
                Tags = tags

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, BlogEditVM request)
        {
            if (id is null) return BadRequest();

            //BlogDetailVM dbBlog = await _blogService.GetByIdAsync((int)id);

            Blog dbBlog = await _context.Blogs.Where(m => m.Id == id)
                                            .Include(m => m.Images)
                                            .Include(m => m.BlogTags)
                                            .ThenInclude(m => m.Tag)
                                            .FirstOrDefaultAsync();

            if (dbBlog is null) return NotFound();


            var selectedTags = dbBlog.BlogTags.Select(m => m.TagId).ToList();




            request.Images = dbBlog.Images;


            foreach (var item in request.Tags)
            {
            }

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            BlogVM existBlog = await _blogService.GetByNameWithoutTrackingAsync(request.Title);


            if (request.Photos != null)
            {
                foreach (var photo in request.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photos", "File can only be in image format");
                        return View(request);

                    }

                    if (!photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photos", "File size can be max 200 kb");
                        return View(request);
                    }


                }
            }


            if (existBlog is not null)
            {
                if (existBlog.Id == request.Id)
                {
                    await _blogService.EditAsync(request);

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("Name", "This name already exists");
                return View(request);
            }

            await _blogService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]

        public async Task<IActionResult> DeleteBlogImage(int id)
        {
            await _blogService.DeleteBlogImageAsync(id);

            return Ok();
        }

    }
}
