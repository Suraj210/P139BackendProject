﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Brand;
using P139BackendProject.Areas.Admin.ViewModels.Product;
using P139BackendProject.Data;
using P139BackendProject.Helpers.Extentions;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BrandService(AppDbContext context,
                             IMapper mapper,
                             IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task CreateAsync(BrandCreateVM brand)
        {
            foreach (var photo in brand.Photos)
            {
                string fileName = $"{Guid.NewGuid()} - {photo.FileName}";
                string path = _env.GetFilePath("img/client", fileName);

                await _context.Brands.AddAsync(new Brand { Image = fileName });
                await _context.SaveChangesAsync();

                await photo.SaveFileAsync(path);
            }
        }

        public async Task DeleteAsync(int id)
        {
            Brand dbBrand = await _context.Brands.FirstOrDefaultAsync(m => m.Id == id);
            _context.Brands.Remove(dbBrand);
            await _context.SaveChangesAsync();

            string path = _env.GetFilePath("img/client", dbBrand.Image);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(BrandEditVM brand)
        {
            string oldPath = _env.GetFilePath("img/client", brand.Image);

            string fileName = $"{Guid.NewGuid()}-{brand.Photo.FileName}";

            string newPath = _env.GetFilePath("img/client", fileName);

            Brand dbBrand = await _context.Brands.FirstOrDefaultAsync(m => m.Id == brand.Id);

            dbBrand.Image = fileName;

            await _context.SaveChangesAsync();

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            await brand.Photo.SaveFileAsync(newPath);
        }


        public async Task<BrandVM> GetByIdAsync(int id)
        {
            return _mapper.Map<BrandVM>(await _context.Brands.FirstOrDefaultAsync(m => m.Id == id));
        }

        public async Task<List<BrandVM>> GetAllAsync()
        {
            List<Brand> brands =await _context.Brands.ToListAsync();

            return _mapper.Map<List<BrandVM>>(brands);
        }
    }
}
