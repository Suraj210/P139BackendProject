﻿using P139BackendProject.Areas.Admin.ViewModels.Layout;
using P139BackendProject.Services.Interfaces;

namespace P139BackendProject.Services
{
    public class LayoutService:ILayoutService
    {
        private readonly ISettingService _settingService;
        public LayoutService(ISettingService settingService)
        {
            _settingService = settingService;
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

        public HeaderVM GetHeaderDatas()
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            return new HeaderVM()
            {
                Logo = settingDatas["HeaderLogo"]
            };
        }
    }
}
