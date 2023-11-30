using P139BackendProject.Areas.Admin.ViewModels.Tag;

namespace P139BackendProject.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<TagVM>> GetAllAsync();
    }
}
