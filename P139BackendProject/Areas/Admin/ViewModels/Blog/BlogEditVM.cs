using Microsoft.AspNetCore.Mvc.Rendering;
using P139BackendProject.Models;
using System.ComponentModel.DataAnnotations;

namespace P139BackendProject.Areas.Admin.ViewModels.Blog
{
    public class BlogEditVM
    {

        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }
        public List<BlogImage> Images { get; set; }
        public List<IFormFile> Photos { get; set; }
        public IList<SelectListItem> Tags { get; set; }

    }
}
