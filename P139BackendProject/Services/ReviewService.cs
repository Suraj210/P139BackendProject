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

        public ReviewService(AppDbContext context,
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ReviewVM>> GetAllAsync()
        {
            List<Review> reviews = await _context.Reviews.Include(m=>m.Customer).ToListAsync();

            return _mapper.Map<List<ReviewVM>>(reviews);

        }
    }
}
