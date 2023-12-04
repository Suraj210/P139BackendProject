using Microsoft.AspNetCore.Identity;
using P139BackendProject.Areas.Admin.ViewModels.Layout;
using P139BackendProject.Models;
using P139BackendProject.Services.Interfaces;
using System.Security.Claims;

namespace P139BackendProject.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly ISettingService _settingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public LayoutService(ISettingService settingService, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _settingService = settingService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public FooterVM GetFooterDatas()
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            return new FooterVM()
            {
                Logo = settingDatas["FooterLogo"],
                Email = settingDatas["Email"],
                Phone = settingDatas["Phone"],
                Eax = settingDatas["Eax"],
                Address = settingDatas["Address"]

            };
        }

        public async Task<HeaderVM> GetHeaderDatas()
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            HeaderVM model = new();

            model.Logo = settingDatas["HeaderLogo"];

            if (userId is not null)
            {
                AppUser currentUser = await _userManager.FindByIdAsync(userId);
                model.UserFullName = currentUser.FullName;
            }

            return model;
        }
    }
}
