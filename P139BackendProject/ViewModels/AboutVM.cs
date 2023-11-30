using P139BackendProject.Areas.Admin.ViewModels.AboutContent;
using P139BackendProject.Areas.Admin.ViewModels.Brand;
using P139BackendProject.Areas.Admin.ViewModels.Team;

namespace P139BackendProject.ViewModels
{
    public class AboutVM
    {
        public List<BrandVM> Brands { get; set; }
        public List<TeamVM> TeamMembers { get; set; }

        public AboutContentVM AboutContent { get; set; }
    }
}
