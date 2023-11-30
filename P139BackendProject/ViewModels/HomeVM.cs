using P139BackendProject.Areas.Admin.ViewModels.Advert;
using P139BackendProject.Areas.Admin.ViewModels.Blog;
using P139BackendProject.Areas.Admin.ViewModels.Product;
using P139BackendProject.Areas.Admin.ViewModels.Review;
using P139BackendProject.Areas.Admin.ViewModels.Slider;

namespace P139BackendProject.ViewModels
{
    public class HomeVM
    {
      public List<AdvertVM> Adverts { get; set; }
      public List<SliderVM> Sliders { get; set; }
      public List<ReviewVM> Reviews { get; set; }
      public List<ProductVM> Products { get; set; }
      public List<BlogVM> Blogs { get; set; }

    }
}
