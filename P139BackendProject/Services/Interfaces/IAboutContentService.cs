using P139BackendProject.Areas.Admin.ViewModels.AboutContent;
using P139BackendProject.ViewModels;

namespace P139BackendProject.Services.Interfaces
{
    public interface IAboutContentService
    {
        Task<AboutContentVM> GetDataAsync();
        Task<AboutContentVM> GetByIdAsync(int id);
        Task EditAsync(AboutContentEditVM request);
    }
}
