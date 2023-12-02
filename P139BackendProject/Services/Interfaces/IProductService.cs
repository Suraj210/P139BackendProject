using P139BackendProject.Areas.Admin.ViewModels.Product;
using P139BackendProject.Models;

namespace P139BackendProject.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllAsync();
        Task<List<ProductVM>> GetAllByTakeAsync(int take);
        Task<List<ProductVM>> GetLoadedProductsAsync(int skipCount, int take);
        Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take);
        Task<int> GetProductCountAsync();
        Task<List<ProductVM>> SearchAsync(string searchText);
        Task<Product> GetByIdAsync(int id);
        Task<List<ProductVM>> GetByTakeWithIncludes(int take);
        Task<ProductVM> GetByIdWithIncludesAsync(int id);
        Task DeleteAsync(int id);
    }
}
