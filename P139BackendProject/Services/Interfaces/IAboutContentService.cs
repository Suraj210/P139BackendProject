using P139BackendProject.Areas.Admin.ViewModels.AboutContent;

namespace P139BackendProject.Services.Interfaces
{
    public interface IAboutContentService
    {
        Task<AboutContentVM> GetDataAsync();
    }
}
