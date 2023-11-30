using P139BackendProject.Areas.Admin.ViewModels.Brand;
using P139BackendProject.Areas.Admin.ViewModels.Team;

namespace P139BackendProject.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandVM>> GetAllAsync();

    }
}
