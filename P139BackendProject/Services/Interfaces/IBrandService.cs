using P139BackendProject.Areas.Admin.ViewModels.Brand;

namespace P139BackendProject.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandVM>> GetAllAsync();
        Task<BrandVM> GetByIdAsync(int id);
        Task CreateAsync(BrandCreateVM brand);
        Task DeleteAsync(int id);
        Task EditAsync(BrandEditVM brand);

    }
}
