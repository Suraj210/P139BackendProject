using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.AboutContent;
using P139BackendProject.Areas.Admin.ViewModels.Product;
using P139BackendProject.Data;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class AboutContentService : IAboutContentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AboutContentService(AppDbContext context,
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AboutContentVM> GetDataAsync()
        {
            AboutContent aboutContent = await _context.AboutContents.FirstOrDefaultAsync();

            return _mapper.Map<AboutContentVM>(aboutContent);
        }
    }
}
