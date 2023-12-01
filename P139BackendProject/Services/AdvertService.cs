using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Advert;
using P139BackendProject.Data;
using P139BackendProject.Helpers.Extentions;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AdvertService(AppDbContext context,
                             IMapper mapper,
                             IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;  
            _env = env;
        }
        public async Task<List<AdvertVM>> GetAllAsync()
        {
            List<Advert> datas = await _context.Adverts.ToListAsync();

            return _mapper.Map<List<AdvertVM>>(datas);

        }
        public async Task CreateAsync(AdvertCreateVM request)
        {
            string fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

            string path = _env.GetFilePath("img/banner", fileName); //root change

            Advert entity = _mapper.Map<Advert>(request);

            entity.Image = fileName;

            await _context.Adverts.AddAsync(entity);
            await _context.SaveChangesAsync();


            await request.Photo.SaveFileAsync(path);
        }

        public async Task DeleteAsync(int id)
        {
            Advert dbAdvert = await _context.Adverts.FirstOrDefaultAsync(m => m.Id == id);


            _context.Adverts.Remove(dbAdvert);
            await _context.SaveChangesAsync();


            string path = _env.GetFilePath("img/banner", dbAdvert.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(AdvertEditVM request)
        {
            string oldPath = _env.GetFilePath("img/banner", request.Image);

            string fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

            string newPath = _env.GetFilePath("img/banner", fileName);

            Advert dbAdvert = await _context.Adverts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbAdvert);

            dbAdvert.Image = fileName;

            _context.Adverts.Update(dbAdvert);
            await _context.SaveChangesAsync();



            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            await request.Photo.SaveFileAsync(newPath);
        }

        public async Task<AdvertVM> GetByIdAsync(int id)
        {
            var datas = await _context.Adverts.FirstOrDefaultAsync(m => m.Id == id);
            AdvertVM advert = _mapper.Map<AdvertVM>(datas);
            return advert;
        }
    }
}
