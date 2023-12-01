using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Team;
using P139BackendProject.Data;
using P139BackendProject.Helpers.Extentions;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class TeamService : ITeamService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public TeamService(AppDbContext context,
                             IMapper mapper,
                             IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task CreateAsync(TeamCreateVM team)
        {
            string fileName = $"{Guid.NewGuid()}-{team.Image.FileName}";
            string path = _env.GetFilePath("img/team", fileName);


            Team newTeam = _mapper.Map<Team>(team);
            newTeam.Image = fileName;


            await _context.Teams.AddAsync(newTeam);

            await _context.SaveChangesAsync();

            await team.Image.SaveFileAsync(path);
        }

        public async Task Delete(int id)
        {
            Team team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();


            string path = _env.GetFilePath("img/team", team.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(TeamEditVM team)
        {
            Team dbteam = await _context.Teams.FirstOrDefaultAsync(m => m.Id == team.Id);

            if (dbteam.Image != null)
            {

                string oldPath = _env.GetFilePath("img/team", team.Image);
                string fileName = $"{Guid.NewGuid()}-{team.Photo.FileName}";
                string newPath = _env.GetFilePath("img/team", fileName);
                dbteam.Image = fileName;

                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }

                await team.Photo.SaveFileAsync(newPath);

            }
            dbteam.FullName = team.FullName;
            dbteam.Position = team.Position;


            _context.Teams.Update(dbteam);
            await _context.SaveChangesAsync();

           
        }

        public async Task<List<TeamVM>> GetAllAsync()
        {
            List<Team> teams = await _context.Teams.ToListAsync();

            return _mapper.Map<List<TeamVM>>(teams);
        }

        public async Task<TeamVM> GetByIdAsync(int id)
        {
            return _mapper.Map<TeamVM>(await _context.Teams.Where(m => m.Id == id).FirstOrDefaultAsync());
        }
    }
}
