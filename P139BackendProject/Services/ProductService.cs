using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Product;
using P139BackendProject.Data;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context,
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
    }
}
