using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Anime_Streaming_Platform.Models;
using Anime_Streaming_Platform.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Identity.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<User> userManager;

        public RolesController(RoleManager<IdentityRole> roleMgr, UserManager<User> userMrg)
        {
            roleManager = roleMgr;
            userManager = userMrg;
        }

        public IActionResult Index() => View(roleManager.Roles);

        public async Task<IActionResult> Details(string id)
        {
            if (id == null || roleManager.Roles == null)
            {
                return NotFound();
            }

            var role = await roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        public IActionResult Create() => View();
        
        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            return View(name);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || roleManager.Roles == null)
            {
                return NotFound();
            }

            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Admin/Animes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, [Bind("Id,Name")] IdentityRole changedrole)
        {
            if (id != changedrole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await roleManager.FindByIdAsync(changedrole.Id);
                    role.Name = changedrole.Name;
                    var result = await roleManager.UpdateAsync(role);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First().ToString());
                        return View();
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(changedrole);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", roleManager.Roles);
        }
    }
}
