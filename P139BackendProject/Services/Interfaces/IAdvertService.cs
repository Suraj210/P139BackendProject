using P139BackendProject.Areas.Admin.ViewModels.Advert;

namespace P139BackendProject.Services.Interfaces
{
    public interface IAdvertService
    {
        Task<List<AdvertVM>> GetAllAsync();
    }
}
