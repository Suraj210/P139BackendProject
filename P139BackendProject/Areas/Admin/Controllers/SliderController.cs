﻿using Microsoft.AspNetCore.Mvc;
using P139BackendProject.Areas.Admin.ViewModels.Slider;
using P139BackendProject.Data;
using P139BackendProject.Helpers.Extentions;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _env;


        public SliderController(
                                ISliderService sliderService,
                                IWebHostEnvironment env)
        {
            _sliderService = sliderService;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _sliderService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            SliderVM slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            return View(slider);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (!slider.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "File can be only image format");
                return View();
            }

            if (!slider.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photos", "File size can be max 200 kb");
                return View();
            }



            await _sliderService.CreateAsync(slider);


            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sliderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            SliderVM slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            return View(new SliderEditVM { Image = slider.Image, Offer = slider.Offer, Heading = slider.Heading, Description = slider.Description });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SliderEditVM slider, int? id)
        {
            if (id is null) return BadRequest();
            SliderVM dbSlider = await _sliderService.GetByIdAsync((int)id);
            if (dbSlider is null) return NotFound();
            slider.Image = dbSlider.Image;

            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            if (slider.Photo is not null)
            {
                if (!slider.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(slider);
                }
                if (!slider.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "File size can be max 200 kb");
                    return View(slider);
                }
            }
            await _sliderService.EditAsync(slider);
            return RedirectToAction(nameof(Index));
        }
    }
}
