using Microsoft.AspNetCore.Mvc;
using P139BackendProject.Areas.Admin.ViewModels.Contact;
using P139BackendProject.Areas.Admin.ViewModels.Slider;
using P139BackendProject.Services;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> MessageIndex()
        {
            return View(await _contactService.GetAllMessagesAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactService.DeleteAsync(id);
            return RedirectToAction(nameof(MessageIndex));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            ContactMessageVM contactMessage = await _contactService.GetByIdAsync((int)id);

            if (contactMessage is null) return NotFound();

            return View(contactMessage);
        }


    }
}
