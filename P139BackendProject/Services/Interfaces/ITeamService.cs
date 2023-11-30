using P139BackendProject.Areas.Admin.ViewModels.Team;

namespace P139BackendProject.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamVM>> GetAllAsync();
    }
}
