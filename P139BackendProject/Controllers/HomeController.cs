using Microsoft.AspNetCore.Mvc;
using P139BackendProject.Areas.Admin.ViewModels.Advert;
using P139BackendProject.Areas.Admin.ViewModels.Slider;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;
using P139BackendProject.ViewModels;

namespace P139BackendProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdvertService _advertService;
        private readonly ISliderService _sliderService;

        public HomeController(IAdvertService advertService,
                              ISliderService sliderService)
        {
            _advertService = advertService;
            _sliderService = sliderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AdvertVM> adverts = await _advertService.GetAllAsync();
            List<SliderVM> sliders = await _sliderService.GetAllAsync();

            HomeVM model = new()
            {
                Adverts = adverts,
                Sliders = sliders
            };

            return View(model);
        }
    }
}