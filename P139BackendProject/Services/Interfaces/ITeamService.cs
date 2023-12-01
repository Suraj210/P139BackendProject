using P139BackendProject.Areas.Admin.ViewModels.Team;

namespace P139BackendProject.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamVM>> GetAllAsync();
        Task<TeamVM> GetByIdAsync(int id);
        Task Delete(int id);
        Task CreateAsync(TeamCreateVM vm);
        Task EditAsync(TeamEditVM slider);
    }
}
