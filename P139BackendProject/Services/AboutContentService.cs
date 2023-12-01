using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.AboutContent;
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



        public async Task<AboutContentVM> GetByIdAsync(int id)
        {
            var datas = await _context.AboutContents.FirstOrDefaultAsync(m => m.Id == id);
            AboutContentVM about = _mapper.Map<AboutContentVM>(datas);
            return about;
        }

        public async Task<AboutContentVM> GetDataAsync()
        {
            AboutContent aboutContent = await _context.AboutContents.FirstOrDefaultAsync();

            return _mapper.Map<AboutContentVM>(aboutContent);
        }

        public async Task EditAsync(AboutContentEditVM request)
        {
            AboutContent dbAboutContent = await _context.AboutContents.FirstOrDefaultAsync(m => m.Id == request.Id);

            _mapper.Map(request, dbAboutContent);
            await _context.SaveChangesAsync();
        }

 
    }
}
