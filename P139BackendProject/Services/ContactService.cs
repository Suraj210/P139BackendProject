﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Advert;
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
        private readonly IMapper _mapper;

        public ContactService(AppDbContext context,
                              ISettingService settingService,
                              IMapper mapper)
        {
            _context = context;
            _settingService = settingService;
            _mapper = mapper;
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

        public async Task CreateAsync(ContactMessageCreateVM contact)
        {
            var data = _mapper.Map<ContactMessage>(contact);
            await _context.ContactMessages.AddAsync(data);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            ContactMessage dbContactMessage = await _context.ContactMessages.FirstOrDefaultAsync(m => m.Id == id);
            _context.ContactMessages.Remove(dbContactMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ContactMessageVM>> GetAllMessagesAsync()
        {
            return _mapper.Map<List<ContactMessageVM>>(await _context.ContactMessages.ToListAsync());
        }

        public async Task<ContactMessageVM> GetMessageByIdAsync(int id)
        {
            var datas = await _context.ContactMessages.FirstOrDefaultAsync(m => m.Id == id);
            ContactMessageVM contactMessage = _mapper.Map<ContactMessageVM>(datas);
            return contactMessage;
        }

        public async Task<ContactInfoVM> GetInfoAsync()
        {
            return _mapper.Map<ContactInfoVM>(await _context.ContactInfos.FirstOrDefaultAsync());
        }

        public async Task EditInfoAsync(ContactInfoEditVM contact)
        {
            ContactInfo dbContactInfo = await _context.ContactInfos.FirstOrDefaultAsync(m => m.Id == contact.Id);

            _mapper.Map(contact, dbContactInfo);
            await _context.SaveChangesAsync();
        }

        public async Task<ContactInfoVM> GetInfoByIdAsync(int id)
        {
            var datas = await _context.ContactInfos.FirstOrDefaultAsync(m => m.Id == id);
            ContactInfoVM contactInfo = _mapper.Map<ContactInfoVM>(datas);
            return contactInfo;
        }
    }
}
