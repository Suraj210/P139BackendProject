using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Contact;
using P139BackendProject.Data;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class ContactService:IContactService
    {

        private readonly AppDbContext _context;
        private readonly ISettingService _settingService;

        public ContactService(AppDbContext context,
                              ISettingService settingService)
        {
            _context = context;
            _settingService = settingService;
        }

        public async Task<ContactVM> GetDataAsync()
        {

            ContactInfo contact = await _context.ContactInfos.FirstOrDefaultAsync();

            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            ContactVM model = new()
            {
                Description = contact.Description,
                Email = settingDatas["Email"],
                Phone = settingDatas["Phone"],
                Address = settingDatas["Address"]
            };

            return model;
        }
    }
}
