using P139BackendProject.Areas.Admin.ViewModels.Contact;

namespace P139BackendProject.Services.Interfaces
{
    public interface IContactService
    {
        Task<ContactVM> GetDataAsync();
    }
}
