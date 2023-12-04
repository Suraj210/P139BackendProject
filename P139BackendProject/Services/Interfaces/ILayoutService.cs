using P139BackendProject.Areas.Admin.ViewModels.Layout;

namespace P139BackendProject.Services.Interfaces
{
    public interface ILayoutService
    {
        FooterVM GetFooterDatas();
        Task<HeaderVM> GetHeaderDatas();
    }
}
