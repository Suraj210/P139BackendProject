using P139BackendProject.Areas.Admin.ViewModels.Subscribe;

namespace P139BackendProject.Services.Interfaces
{
    public interface ISubscribeService
    {
        Task<List<SubscribeVM>> GetAllAsync();
        Task DeleteAsync(int id);
        Task CreateAsync(SubscribeCreateVM subscribe);
    }
}
