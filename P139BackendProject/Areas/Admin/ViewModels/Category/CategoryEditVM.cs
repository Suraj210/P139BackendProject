using System.ComponentModel.DataAnnotations;

namespace P139BackendProject.Areas.Admin.ViewModels.Category
{
    public class CategoryEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
