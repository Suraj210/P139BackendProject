using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Category;
using P139BackendProject.Data;
using P139BackendProject.Helpers.Extentions;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class CategoryService : ICategoryService
    {


        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CategoryService(AppDbContext context,
                             IMapper mapper,
                             IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public async Task CreateAsync(CategoryCreateVM category)
        {
            Category dbCategory = _mapper.Map<Category>(category);

            await _context.Categories.AddAsync(dbCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category dbCategory = await _context.Categories.Where(m => m.Id == id)
                                                           .Include(m => m.Products)
                                                           .ThenInclude(m => m.Images)
                                                           .FirstOrDefaultAsync();
            _context.Categories.Remove(dbCategory);
            await _context.SaveChangesAsync();

            foreach (var product in dbCategory.Products)
            {
                foreach (var image in product.Images)
                {
                    string path = _env.GetFilePath("img/product/", image.Image);

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
            }

        }

        public async Task EditAsync(CategoryEditVM category)
        {
            Category dbCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == category.Id);

            _mapper.Map(category, dbCategory);

            _context.Categories.Update(dbCategory);

            await _context.SaveChangesAsync();
        }

        public async Task<List<CategoryVM>> GetAllAsync()
        {
            return _mapper.Map<List<CategoryVM>>(await _context.Categories.ToListAsync());
        }

        public async Task<CategoryVM> GetByIdWithoutTrackingAsync(int id)
        {
            return _mapper.Map<CategoryVM>(await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id));
        }

        public async Task<CategoryVM> GetByNameWithoutTrackingAsync(string name)
        {
            return _mapper.Map<CategoryVM>(await _context.Categories.AsNoTracking()
                                                         .FirstOrDefaultAsync(m => m.Name.Trim().ToLower() == name.Trim().ToLower()));
        }
    }
}
