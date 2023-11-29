using P139BackendProject.Areas.Admin.ViewModels.Review;

namespace P139BackendProject.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewVM>> GetAllAsync();
    }
}
