﻿using Microsoft.AspNetCore.Mvc;
using P139BackendProject.Services;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscribeController : Controller
    {
        private readonly ISubscribeService _subscribeService;


        public SubscribeController( ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _subscribeService.GetAllAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _subscribeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
