using Microsoft.AspNetCore.Mvc;
using P139BackendProject.Areas.Admin.ViewModels.Advert;
using P139BackendProject.Areas.Admin.ViewModels.Blog;
using P139BackendProject.Areas.Admin.ViewModels.Product;
using P139BackendProject.Areas.Admin.ViewModels.Review;
using P139BackendProject.Areas.Admin.ViewModels.Slider;
using P139BackendProject.Areas.Admin.ViewModels.Subscribe;
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
        private readonly IBlogService _blogService;
        private readonly ISubscribeService _subscribeService;

        public HomeController(IAdvertService advertService,
                              ISliderService sliderService,
                              IReviewService reviewService,
                              IProductService productService,
                              IBlogService blogService,
                              ISubscribeService subscribeService)
        {
            _advertService = advertService;
            _sliderService = sliderService;
            _reviewService = reviewService;
            _productService = productService;
            _blogService = blogService;
            _subscribeService = subscribeService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AdvertVM> adverts = await _advertService.GetAllAsync();
            List<SliderVM> sliders = await _sliderService.GetAllAsync();
            List<ReviewVM> reviews = await _reviewService.GetAllAsync();
            List<ProductVM> products = await _productService.GetAllByTakeAsync(3);
            List<BlogVM> blogs = await _blogService.GetByTakeWithImagesAsync(3);

            int productCount = await _productService.GetProductCountAsync();

            ViewBag.count = productCount;

            HomeVM model = new()
            {
                Adverts = adverts,
                Sliders = sliders,
                Reviews = reviews,
                Products = products,
                Blogs = blogs,
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> LoadMore(int skipCount)
        {

            List<ProductVM> products = await _productService.GetLoadedProductsAsync(skipCount, 3);

            return PartialView("_ProductsPartial", products);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubscribe(SubscribeCreateVM subscribe)
        {

            await _subscribeService.CreateAsync(subscribe);
            return RedirectToAction("Index", "Home");
        }
    }
}