using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Advert;
using P139BackendProject.Areas.Admin.ViewModels.Product;
using P139BackendProject.Areas.Admin.ViewModels.Review;
using P139BackendProject.Areas.Admin.ViewModels.Slider;
using P139BackendProject.Data;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;
using P139BackendProject.ViewModels;

namespace P139BackendProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdvertService _advertService;
        private readonly ISliderService _sliderService;
        private readonly IReviewService _reviewService;
        private readonly IProductService _productService;
        private readonly AppDbContext _context;

        public HomeController(IAdvertService advertService,
                              ISliderService sliderService,
                              IReviewService reviewService,
                              IProductService productService,
                              AppDbContext context)
        {
            _advertService = advertService;
            _sliderService = sliderService;
            _reviewService = reviewService;
            _productService = productService;
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AdvertVM> adverts = await _advertService.GetAllAsync();
            List<SliderVM> sliders = await _sliderService.GetAllAsync();
            List<ReviewVM> reviews = await _reviewService.GetAllAsync();
            List<ProductVM> products = await _productService.GetAllByTakeAsync(3);

            int productCount = await _context.Products.CountAsync();

            ViewBag.count = productCount;

            HomeVM model = new()
            {
                Adverts = adverts,
                Sliders = sliders,
                Reviews = reviews,
                Products = products
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> LoadMore(int skipCount)
        {

            List<ProductVM> products = await _productService.GetLoadedProductsAsync(skipCount, 3);

            return PartialView("_ProductsPartial", products);
        }
    }
}