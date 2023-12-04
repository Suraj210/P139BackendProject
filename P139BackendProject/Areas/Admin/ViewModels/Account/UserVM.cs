namespace P139BackendProject.Areas.Admin.ViewModels.Account
{
    public class UserVM
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IList<string> RoleNames { get; set; }
    }
}
