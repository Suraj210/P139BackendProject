namespace P139BackendProject.Models
{
    public class Customer:BaseEntity
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public List<Review> Messages { get; set; }
    }
}
