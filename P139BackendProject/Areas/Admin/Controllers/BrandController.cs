﻿using Microsoft.AspNetCore.Mvc;
using P139BackendProject.Areas.Admin.ViewModels.Brand;
using P139BackendProject.Helpers.Extentions;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BrandVM> brands = await _brandService.GetAllAsync();

            return View(brands);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            BrandVM brand=  await _brandService.GetByIdAsync((int)id);

            if (brand is null) return NotFound();

            return View(brand);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var photo in request.Photos)
            {
                if (!photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photos", "File can be only image format");
                    return View();
                }
                if (!photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photos", "File size can be max 200 kb");
                    return View();
                }
            }

            await _brandService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _brandService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            BrandVM brand = await _brandService.GetByIdAsync((int)id);
            if (brand is null) return NotFound();

            return View(new BrandEditVM
            {
                Image = brand.Image
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BrandEditVM request)
        {
            if (id is null) return BadRequest();

            BrandVM dbBrand = await _brandService.GetByIdAsync((int)id);

            if (dbBrand is null) return NotFound();

            request.Image = dbBrand.Image;

            if (request.Photo is null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (!request.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File can be only image format");
                return View(request);
            }

            if (!request.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "File size can be max 200 kb");
                return View(request);
            }

            await _brandService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
