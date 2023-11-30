using System.ComponentModel.DataAnnotations;

namespace P139BackendProject.Areas.Admin.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public string Offer { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
