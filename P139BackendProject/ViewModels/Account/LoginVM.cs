using System.ComponentModel.DataAnnotations;

namespace P139BackendProject.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        public string EmailOrUsername { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
