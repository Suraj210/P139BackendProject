using Microsoft.AspNetCore.Mvc;
using P139BackendProject.Areas.Admin.ViewModels.Contact;
using P139BackendProject.Services.Interfaces;
using P139BackendProject.ViewModels;

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


            ContactPageVM model = new()
            {
                Contact = contact,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateMessage(ContactMessageCreateVM request)
        {

            await _contactService.CreateAsync(request);

            return RedirectToAction("Index", "Contact");

        }
    }

}
