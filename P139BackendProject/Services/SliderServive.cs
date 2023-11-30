using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Slider;
using P139BackendProject.Data;
using P139BackendProject.Helpers.Extentions;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class SliderServive : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SliderServive(AppDbContext context,
                             IMapper mapper,
                             IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task CreateAsync(SliderCreateVM slider)
        {
            string fileName = $"{Guid.NewGuid()}-{slider.Photo.FileName}";

            string path = _env.GetFilePath("img/hero", fileName);

            var data = _mapper.Map<Slider>(slider);

            data.Image = fileName;

            await _context.AddAsync(data);

            await _context.SaveChangesAsync();

            await slider.Photo.SaveFileAsync(path);

        }

        public async Task DeleteAsync(int id)
        {
            Slider slider = await _context.Sliders.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();

            string path = _env.GetFilePath("img/hero/", slider.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(SliderEditVM slider)
        {
            Slider dbSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == slider.Id);
            if (slider.Photo != null)
            {

                string oldPath = _env.GetFilePath("img/hero/", slider.Image);

                string fileName = $"{Guid.NewGuid()}-{slider.Photo.FileName}";

                string newPath = _env.GetFilePath("img/hero/", fileName);
                dbSlider.Image = fileName;

                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }

                await slider.Photo.SaveFileAsync(newPath);

            }
            dbSlider.Offer = slider.Offer;
            dbSlider.Heading = slider.Heading;
            dbSlider.Description = slider.Description;
            await _context.SaveChangesAsync();
        }

        public async Task<List<SliderVM>> GetAllAsync()
        {
            List<Slider> datas = await _context.Sliders.ToListAsync();

            return _mapper.Map<List<SliderVM>>(datas);
        }

        public async Task<SliderVM> GetByIdAsync(int id)
        {
            Slider slider =await _context.Sliders.Where(m=>m.Id == id).FirstOrDefaultAsync();

            return _mapper.Map<SliderVM>(slider);
        }
    }
}
