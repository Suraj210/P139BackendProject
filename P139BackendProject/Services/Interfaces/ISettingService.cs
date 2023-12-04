using P139BackendProject.Areas.Admin.ViewModels.Setting;
using P139BackendProject.Models;

namespace P139BackendProject.Services.Interfaces
{
    public interface ISettingService
    {
        Dictionary<string, string> GetSettings();
        Task<List<SettingVM>> GetAllAsync();
        Task<SettingVM> GetByIdAsync(int id);
        Task EditAsync(SettingEditVM setting);
    }
}
