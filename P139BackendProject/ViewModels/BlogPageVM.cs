using P139BackendProject.Areas.Admin.ViewModels.Blog;
using P139BackendProject.Areas.Admin.ViewModels.Tag;
using P139BackendProject.Helpers;

namespace P139BackendProject.ViewModels
{
    public class BlogPageVM
    {
        public Paginate<BlogVM> PaginatedDatas { get; set; }
        public List<TagVM> Tags { get; set; }
    }
}
