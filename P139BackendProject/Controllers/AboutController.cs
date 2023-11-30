using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using P139BackendProject.Areas.Admin.ViewModels.AboutContent;
using P139BackendProject.Areas.Admin.ViewModels.Brand;
using P139BackendProject.Areas.Admin.ViewModels.Team;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;
using P139BackendProject.ViewModels;

namespace P139BackendProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutContentService _aboutService;
        private readonly IBrandService _brandService;
        private readonly ITeamService _teamService ;

        public AboutController(IAboutContentService aboutService,
                               IBrandService brandService,
                               ITeamService teamService)
        {
            _aboutService = aboutService;
            _brandService = brandService;
            _teamService = teamService;

        }
        public async Task<IActionResult> Index()
        {
            AboutContentVM aboutContent = await _aboutService.GetDataAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();
            List<TeamVM> teamMembers = await _teamService.GetAllAsync();


            AboutVM model = new()
            {
                AboutContent = aboutContent,
                Brands = brands,
                TeamMembers = teamMembers
            };

            return View(model);
        }
    }
}
