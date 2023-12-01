using P139BackendProject.Areas.Admin.ViewModels.Contact;

namespace P139BackendProject.ViewModels
{
    public class ContactPageVM
    {
        public ContactVM Contact { get; set; }
        public ContactMessageCreateVM NewContact { get; set; }
    }
}
