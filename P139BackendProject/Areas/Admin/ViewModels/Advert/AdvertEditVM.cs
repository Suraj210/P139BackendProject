﻿using System.ComponentModel.DataAnnotations;

namespace P139BackendProject.Areas.Admin.ViewModels.Advert
{
    public class AdvertEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Offer { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
