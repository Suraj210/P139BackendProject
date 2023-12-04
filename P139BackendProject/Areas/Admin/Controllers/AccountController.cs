using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using P139BackendProject.Areas.Admin.ViewModels.Account;
using P139BackendProject.Models;

namespace P139BackendProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dbUsers = await _userManager.Users
                                          //.Select(m => new UserVM { FullName = m.FullName, Email = m.Email, Username = m.UserName })
                                          .ToListAsync();

            List<UserVM> users = new();
            foreach (var user in dbUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                users.Add(new UserVM
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Username = user.UserName,
                    RoleNames = roles
                });
            }

            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> AddRoleToUser()
        {
            ViewBag.roles = await GetRolesAsync();
            ViewBag.users = await GetUsersAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser(UserRoleVM request)
        {
            AppUser user = await _userManager.FindByIdAsync(request.UserId);

            IdentityRole role = await _roleManager.FindByIdAsync(request.RoleId);

            await _userManager.AddToRoleAsync(user, role.Name);

            return RedirectToAction("Index", "Dashboard");
        }

        private async Task<SelectList> GetRolesAsync()
        {
            return new SelectList(await _roleManager.Roles.ToListAsync(), "Id", "Name");
        }

        private async Task<SelectList> GetUsersAsync()
        {
            return new SelectList(await _userManager.Users.ToListAsync(), "Id", "UserName");
        }

    }
}
