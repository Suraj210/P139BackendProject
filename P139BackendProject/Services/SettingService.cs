﻿using P139BackendProject.Data;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class SettingService:ISettingService
    {
        private readonly AppDbContext _context;
        public SettingService(AppDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        }
    }
}
