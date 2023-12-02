using P139BackendProject.Areas.Admin.ViewModels.Review;

namespace P139BackendProject.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewVM>> GetAllAsync();

        Task<ReviewVM> GetByIdWithIncludeAsync(int id);
        Task DeleteAsync(int id);
    }
}
