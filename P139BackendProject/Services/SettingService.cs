using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Setting;
using P139BackendProject.Data;
using P139BackendProject.Helpers.Extentions;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class SettingService:ISettingService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public SettingService(AppDbContext context,IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        }


        public async Task<List<SettingVM>> GetAllAsync()
        {
            var datas = await _context.Settings.ToListAsync();
            return _mapper.Map<List<SettingVM>>(datas);
        }


        public async Task<SettingVM> GetByIdAsync(int id)
        {
            var data = await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<SettingVM>(data);
        }

        public async Task EditAsync(SettingEditVM setting)
        {
            if (setting.Value.Contains("jpg") || setting.Value.Contains("png") || setting.Value.Contains("jpeg"))
            {
                string oldPath = _env.GetFilePath("img", setting.Value);

                string fileName = $"{Guid.NewGuid()}-{setting.Photo.FileName}";

                string newPath = _env.GetFilePath("img", fileName);

                Setting dbSetting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == setting.Id);

                dbSetting.Value = fileName;

                await _context.SaveChangesAsync();

                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }

                await setting.Photo.SaveFileAsync(newPath);
            }
            else
            {
                Setting dbSetting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == setting.Id);

                _mapper.Map(setting, dbSetting);

                _context.Settings.Update(dbSetting);

                await _context.SaveChangesAsync();
            }

        }
    }
}
