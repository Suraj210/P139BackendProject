﻿using Microsoft.AspNetCore.Mvc;
using P139BackendProject.Areas.Admin.ViewModels.Contact;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            ContactVM contact = await _contactService.GetDataAsync();

            return View(contact);
        }
    }

}
