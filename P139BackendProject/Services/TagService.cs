﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Tag;
using P139BackendProject.Data;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class TagService:ITagService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TagService(AppDbContext context,
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(TagCreateVM tag)
        {
            Tag dbTag = _mapper.Map<Tag>(tag);

            await _context.Tags.AddAsync(dbTag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Tag dbTag = await _context.Tags.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Tags.Remove(dbTag);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TagEditVM tag)
        {
            Tag dbTag = await _context.Tags.AsNoTracking().FirstOrDefaultAsync(m => m.Id == tag.Id);

            _mapper.Map(tag, dbTag);

            _context.Tags.Update(dbTag);

            await _context.SaveChangesAsync();
        }

        public async Task<List<TagVM>> GetAllAsync()
        {
            return _mapper.Map<List<TagVM>>(await _context.Tags.ToListAsync());
        }

        public async Task<TagVM> GetByIdWithoutTrackingAsync(int id)
        {
            return _mapper.Map<TagVM>(await _context.Tags.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id));
        }

        public async Task<TagVM> GetByNameWithoutTrackingAsync(string name)
        {
            return _mapper.Map<TagVM>(await _context.Tags.AsNoTracking()
                                                         .FirstOrDefaultAsync(m => m.Name.Trim().ToLower() == name.Trim().ToLower()));
        }

        public List<SelectListItem> GetAllSelectedAsync()
        {
            return _context.Tags.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString(),

            }).ToList();
        }
    }
}
