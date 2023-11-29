using P139BackendProject.Areas.Admin.ViewModels.Slider;

namespace P139BackendProject.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();
    }
}
