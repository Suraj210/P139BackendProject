using P139BackendProject.Models;

namespace P139BackendProject.Areas.Admin.ViewModels.Blog
{
    public class BlogDetailVM
    {
        public DateTime CreateTime { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<BlogImage> Images { get; set; }
        public List<P139BackendProject.Models.Tag> Tags { get; set; }

    }
}
