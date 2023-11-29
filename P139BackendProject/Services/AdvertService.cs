using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Advert;
using P139BackendProject.Data;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AdvertService(AppDbContext context,
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }
        public async Task<List<AdvertVM>> GetAllAsync()
        {
            List<Advert> datas = await _context.Adverts.ToListAsync();

            return _mapper.Map<List<AdvertVM>>(datas);

        }
    }
}
