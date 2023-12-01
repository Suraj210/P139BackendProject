using System.ComponentModel.DataAnnotations;

namespace P139BackendProject.Areas.Admin.ViewModels.Brand
{
    public class BrandCreateVM
    {
        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}
