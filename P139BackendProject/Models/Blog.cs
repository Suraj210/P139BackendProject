namespace P139BackendProject.Models
{
    public class Blog:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<BlogImage> Images { get; set; }
        public ICollection<BlogTag> BlogTags { get; set; } = new HashSet<BlogTag>();
    }
}
