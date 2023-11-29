using AutoMapper;
using P139BackendProject.Areas.Admin.ViewModels.Advert;
using P139BackendProject.Areas.Admin.ViewModels.Product;
using P139BackendProject.Areas.Admin.ViewModels.Review;
using P139BackendProject.Areas.Admin.ViewModels.Slider;
using P139BackendProject.Models;

namespace P139BackendProject.Helpers.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Advert, AdvertVM>();
            CreateMap<Slider, SliderVM>();
            CreateMap<Review, ReviewVM>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Customer.FullName))
                                         .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Customer.Image));


            CreateMap<Product, ProductVM>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                                          .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(m => m.IsMain).Image));


        }
    }
}
