using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Product;
using P139BackendProject.Data;
using P139BackendProject.Helpers.Extentions;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ProductService(AppDbContext context,
                             IMapper mapper,
                             IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public async Task<List<ProductVM>> GetAllAsync()
        {
            List<Product> products = await _context.Products.Include(m=>m.Category).Include(m=>m.Images).ToListAsync();

            return _mapper.Map<List<ProductVM>>(products);
        }
        public async Task<List<ProductVM>> GetAllByTakeAsync(int take)
        {
            List<Product> products = await _context.Products.Include(m => m.Images)
                                                            .Include(m=>m.Category)
                                                            .Take(take)
                                                            .ToListAsync();

            return _mapper.Map<List<ProductVM>>(products);

        }
        public async Task<List<ProductVM>> GetLoadedProductsAsync(int skipCount, int take)
        {
            List<Product> products = await _context.Products.Include(m => m.Images)
                                                            .Include(m=>m.Category)                         
                                                            .Skip(skipCount)
                                                            .Take(take)
                                                            .ToListAsync();

            return _mapper.Map<List<ProductVM>>(products);

        }
        public async Task<int> GetProductCountAsync()
        {
            return await _context.Products.CountAsync();
        }
        public async Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take)
        {
            List<Product> products = await _context.Products.Include(m => m.Category)
                                                             .Include(m => m.Images)
                                                             .Skip((page * take) - take)
                                                             .Take(take)
                                                             .ToListAsync();
            return _mapper.Map<List<ProductVM>>(products);
        }
        public async Task<List<ProductVM>> SearchAsync(string searchText)
        {
            var dbProducts = await _context.Products.Include(m => m.Images)
                                                 .Include(m => m.Category)
                                                 .OrderByDescending(m => m.Id)
                                                 .Where(m => m.Name.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                                                 .Take(6)
                                                 .ToListAsync();

            return _mapper.Map<List<ProductVM>>(dbProducts);
        }
        public async Task<Product> GetByIdAsync(int id) => await _context.Products.FindAsync(id);
        public async Task<List<ProductVM>> GetByTakeWithIncludes(int take)
        {
            return _mapper.Map<List<ProductVM>>(await _context.Products.Include(m => m.Category)
                                                                      .Include(m => m.Images)
                                                                      .Take(take).ToListAsync());
        }
        public async Task<ProductVM> GetByIdWithIncludesAsync(int id)
        {
            return _mapper.Map <ProductVM>(await _context.Products.Where(m => m.Id == id)
                                                                  .Include(m => m.Images)
                                                                  .Include(m => m.Category)
                                                                  .FirstOrDefaultAsync());
        }
        public async Task DeleteAsync(int id)
        {
            Product dbproduct = await _context.Products.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);


            _context.Products.Remove(dbproduct);
            await _context.SaveChangesAsync();


            foreach (var photo in dbproduct.Images)
            {

                string path = _env.GetFilePath("img/product", photo.Image);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }


            }
        }
    }
}
