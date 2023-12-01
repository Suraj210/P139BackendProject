using System.ComponentModel.DataAnnotations;

namespace P139BackendProject.Areas.Admin.ViewModels.Contact
{
    public class ContactInfoEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
