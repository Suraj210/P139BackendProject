using P139BackendProject.Models;

namespace P139BackendProject.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
    }
}
