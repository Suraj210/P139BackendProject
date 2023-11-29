using P139BackendProject.Areas.Admin.ViewModels.Product;

namespace P139BackendProject.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllAsync();
        Task<List<ProductVM>> GetAllByTakeAsync(int take);
        Task<List<ProductVM>> GetLoadedProductsAsync(int skipCount, int take);

    }
}
