using P139BackendProject.Models;
using System.ComponentModel.DataAnnotations;

namespace P139BackendProject.Areas.Admin.ViewModels.Product
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<ProductImage> Images { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
