using P139BackendProject.Areas.Admin.ViewModels.Contact;

namespace P139BackendProject.Services.Interfaces
{
    public interface IContactService
    {
        Task<ContactVM> GetDataAsync();
        Task<List<ContactMessageVM>> GetAllMessagesAsync();
        Task CreateAsync(ContactMessageCreateVM contact);
        Task DeleteAsync(int id);
        Task<ContactMessageVM> GetByIdAsync(int id);
    }
}
