using P139BackendProject.Areas.Admin.ViewModels.Advert;

namespace P139BackendProject.Services.Interfaces
{
    public interface IAdvertService
    {
        Task<List<AdvertVM>> GetAllAsync();
        Task CreateAsync(AdvertCreateVM request);
        Task DeleteAsync(int id);
        Task<AdvertVM> GetByIdAsync(int id);
        Task EditAsync(AdvertEditVM request);
    }
}
