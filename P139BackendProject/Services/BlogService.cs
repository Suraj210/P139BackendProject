using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Blog;
using P139BackendProject.Data;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class BlogService:IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BlogService(AppDbContext context,
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BlogDetailVM> GetByIdAsync(int id)
        {
            Blog blog = await _context.Blogs.Where(m => m.Id == id)
                                             .Include(m => m.Images)
                                             .Include(m => m.BlogTags)
                                             .ThenInclude(m => m.Tag)
                                             .FirstOrDefaultAsync();

            return _mapper.Map<BlogDetailVM>(blog);

        }

        public async Task<List<BlogVM>> GetByTakeWithImagesAsync(int take)
        {
            List<Blog> blogs = await _context.Blogs.OrderByDescending(m => m.CreateTime)
                                                   .Take(take)
                                                   .Include(m => m.Images)
                                                   .ToListAsync();

            return _mapper.Map<List<BlogVM>>(blogs);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Blogs.CountAsync();
        }

        public async Task<List<BlogVM>> GetPaginatedDatasAsync(int page, int take)
        {
            List<Blog> blogs = await _context.Blogs.OrderByDescending(m => m.CreateTime)
                                                   .Include(m => m.Images)
                                                   .Skip((page * take) - take)
                                                   .Take(take)
                                                   .ToListAsync();
            return _mapper.Map<List<BlogVM>>(blogs);
        }
    }
}
