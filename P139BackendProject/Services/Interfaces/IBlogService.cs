using P139BackendProject.Areas.Admin.ViewModels.Blog;

namespace P139BackendProject.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetByTakeWithImagesAsync(int take);
        Task<int> GetCountAsync();
        Task<List<BlogVM>> GetPaginatedDatasAsync(int page, int take);
        Task<BlogDetailVM> GetByIdAsync(int id);
        Task<BlogVM> GetByNameWithoutTrackingAsync(string name);
        Task CreateAsync(BlogCreateVM blog);
        Task DeleteAsync(int id);
        Task EditAsync(BlogEditVM blog);
        Task DeleteBlogImageAsync(int id);
    }
}
