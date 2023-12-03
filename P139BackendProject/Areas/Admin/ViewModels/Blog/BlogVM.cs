namespace P139BackendProject.Areas.Admin.ViewModels.Blog
{
    public class BlogVM
    {
        public DateTime CreateTime{ get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<P139BackendProject.Models.Tag> Tags { get; set; }

    }
}
