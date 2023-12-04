using Microsoft.AspNetCore.Identity;

namespace P139BackendProject.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
