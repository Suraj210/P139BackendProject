using System.ComponentModel.DataAnnotations;

namespace P139BackendProject.Areas.Admin.ViewModels.Advert
{
    public class AdvertCreateVM
    {
        [Required]
        public string Offer { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
