﻿using Microsoft.AspNetCore.Mvc;
using P139BackendProject.Areas.Admin.ViewModels.Product;
using P139BackendProject.Helpers;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        
        public async Task<IActionResult> Index(int page = 1, int take = 3)
        {
            List<ProductVM> dbPaginatedDatas = await _productService.GetPaginatedDatasAsync(page, take);

            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

            return View(paginatedDatas);
        }


        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _productService.GetProductCountAsync();
            return (int)Math.Ceiling((decimal)(productCount) / take);
        }

        public IActionResult ProductDetails()
        {
            return View();
        }


        public async Task<IActionResult> Search(string searchText)
        {

            if(searchText == null)
            {
                return RedirectToAction("Index","Home");
            }

            return View(await _productService.SearchAsync(searchText));
        }

    }
}
