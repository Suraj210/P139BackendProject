using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Review;
using P139BackendProject.Data;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ReviewService(AppDbContext context,
                             IMapper mapper,
                             IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task DeleteAsync(int id)
        {
            Review review = await _context.Reviews.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReviewVM>> GetAllAsync()
        {
            List<Review> reviews = await _context.Reviews.Include(m=>m.Customer).ToListAsync();

            return _mapper.Map<List<ReviewVM>>(reviews);

        }

        public async Task<ReviewVM> GetByIdWithIncludeAsync(int id)
        {
            return _mapper.Map<ReviewVM>(await _context.Reviews.Include(m => m.Customer).FirstOrDefaultAsync(m => m.Id == id));
        }
    }
}
