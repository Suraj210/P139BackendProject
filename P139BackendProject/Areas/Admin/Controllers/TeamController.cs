using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Team;
using P139BackendProject.Data;
using P139BackendProject.Helpers.Extentions;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        public TeamController(ITeamService teamService,
                              IWebHostEnvironment env,
                              AppDbContext context)
        {
            _teamService = teamService;
            _env = env;
            _context = context;

        }


        public async Task<IActionResult> Index()
        {
            return View(await _teamService.GetAllAsync());
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id is null) return BadRequest();


            TeamVM team = await _teamService.GetByIdAsync((int)id);

            if (team is null) return NotFound();

            return View(team);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM team)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!team.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "File format can be only image");
                return View();

            }
            if (!team.Image.CheckFileSize(200))
            {
                ModelState.AddModelError("Image", "File size can be max 200kb");
                return View();
            }


          
            await _teamService.CreateAsync(team);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _teamService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Team team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);

            if (team is null) return NotFound();

            TeamEditVM model = new()
            {
                Image = team.Image,
                Position = team.Position,
                FullName = team.FullName
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditAsync(int? id, TeamEditVM team)
        {

            if (id is null) return BadRequest();

            Team dbteam = await _context.Teams.FirstOrDefaultAsync(m => m.Id == team.Id);

            if (dbteam is null) return NotFound();

            team.Image = dbteam.Image;

            if (!ModelState.IsValid)
            {
                return View(team);
            }

            if (team.Photo is null)
            {
                dbteam.FullName = team.FullName;
                dbteam.Position = team.Position;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            if (!team.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File can be only image format");
                return View(team);
            }

            if (!team.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "File size can be max 200kb");
                return View(team);
            }

            await _teamService.EditAsync(team);

            return RedirectToAction(nameof(Index));
        }
    }
}
