namespace P139BackendProject.Models
{
    public class Review:BaseEntity
    {
        public string Message { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
