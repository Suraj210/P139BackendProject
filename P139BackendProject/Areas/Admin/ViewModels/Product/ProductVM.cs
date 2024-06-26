﻿using P139BackendProject.Models;

namespace P139BackendProject.Areas.Admin.ViewModels.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public List<ProductImage> Images { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
