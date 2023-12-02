using P139BackendProject.Areas.Admin.ViewModels.Category;
using P139BackendProject.Models;

namespace P139BackendProject.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAllAsync();
        Task<CategoryVM> GetByIdWithoutTrackingAsync(int id);
        Task DeleteAsync(int id);
        Task CreateAsync(CategoryCreateVM category);
        Task EditAsync(CategoryEditVM category);
        Task<CategoryVM> GetByNameWithoutTrackingAsync(string name);
    }
}
