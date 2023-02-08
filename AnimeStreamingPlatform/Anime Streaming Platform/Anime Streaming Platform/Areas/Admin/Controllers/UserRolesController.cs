using System.Threading.Tasks;
using Anime_Streaming_Platform.Models;
using Anime_Streaming_Platform.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Anime_Streaming_Platform.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserRolesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRoles = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var userRoleViewModel = new UserRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.Email
                };

                var roles = await _userManager.GetRolesAsync(user);
                userRoleViewModel.RoleName = string.Join(", ", roles);
                userRoles.Add(userRoleViewModel);
            }

            return View(userRoles);
        }


        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            var model = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                RoleName = role
            };

            return View(model);
        }




        public async Task<IActionResult> Create()
        {
            var users = await _userManager.Users.ToListAsync();
            ViewData["Users"] = new SelectList(users, "Id", "UserName");

            var roles = await _roleManager.Roles.ToListAsync();
            ViewData["Roles"] = new SelectList(roles, "Name", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add user to selected role");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var model = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                RoleName = roles.SingleOrDefault()
            };

            var allRoles = await _roleManager.Roles.ToListAsync();
            ViewData["Roles"] = new SelectList(allRoles, "Name", "Name", model.RoleName);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user from current role(s)");
                return View(model);
            }

            result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add user to selected role");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            if (role != null)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            return RedirectToAction("Index");
        }






    }
}
