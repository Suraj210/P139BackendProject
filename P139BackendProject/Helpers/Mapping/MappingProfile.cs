using AutoMapper;
using P139BackendProject.Areas.Admin.ViewModels.AboutContent;
using P139BackendProject.Areas.Admin.ViewModels.Advert;
using P139BackendProject.Areas.Admin.ViewModels.Blog;
using P139BackendProject.Areas.Admin.ViewModels.Brand;
using P139BackendProject.Areas.Admin.ViewModels.Contact;
using P139BackendProject.Areas.Admin.ViewModels.Product;
using P139BackendProject.Areas.Admin.ViewModels.Review;
using P139BackendProject.Areas.Admin.ViewModels.Slider;
using P139BackendProject.Areas.Admin.ViewModels.Subscribe;
using P139BackendProject.Areas.Admin.ViewModels.Tag;
using P139BackendProject.Areas.Admin.ViewModels.Team;
using P139BackendProject.Models;

namespace P139BackendProject.Helpers.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Advert, AdvertVM>().ReverseMap();
            CreateMap<Advert, AdvertCreateVM>().ReverseMap();
            CreateMap<Advert, AdvertEditVM>().ReverseMap();
            CreateMap<Slider, SliderVM>().ReverseMap();
            CreateMap<Slider, SliderCreateVM>().ReverseMap();
            CreateMap<Review, ReviewVM>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Customer.FullName))
                                         .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Customer.Image));


            CreateMap<Product, ProductVM>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                                           .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(m => m.IsMain).Image));

            CreateMap<Blog, BlogVM>().ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(m => m.IsMain).Image));
            CreateMap<Blog, BlogDetailVM>().ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.BlogTags.Select(m=>m.Tag).ToList()));
            CreateMap<Tag, TagVM>();

            CreateMap<AboutContent, AboutContentVM>();
            CreateMap<Brand, BrandVM>();
            CreateMap<Team, TeamVM>().ReverseMap();
            CreateMap<Team, TeamCreateVM>().ReverseMap();
            CreateMap<Team, TeamEditVM>().ReverseMap();
            CreateMap<Subscribe, SubscribeVM>().ReverseMap();
            CreateMap<SubscribeCreateVM, Subscribe>().ReverseMap();
            CreateMap<ContactVM, ContactMessageVM>().ReverseMap();
            CreateMap<ContactMessage, ContactMessageVM>().ReverseMap();
            CreateMap<ContactMessageCreateVM, ContactMessage>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoVM>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoEditVM>().ReverseMap();
            CreateMap<AboutContent, AboutContentVM>().ReverseMap();
            CreateMap<AboutContent, AboutContentEditVM>().ReverseMap();




        }
    }
}
