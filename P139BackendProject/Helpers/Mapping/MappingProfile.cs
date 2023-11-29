using AutoMapper;
using P139BackendProject.Areas.Admin.ViewModels.Advert;
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

        }
    }
}
