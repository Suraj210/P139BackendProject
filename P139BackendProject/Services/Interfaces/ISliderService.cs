using P139BackendProject.Areas.Admin.ViewModels.Slider;

namespace P139BackendProject.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();
        Task<SliderVM> GetByIdAsync(int id);
        Task CreateAsync(SliderCreateVM slider);
        Task DeleteAsync(int id);

        Task EditAsync(SliderEditVM slider);
    }
}
