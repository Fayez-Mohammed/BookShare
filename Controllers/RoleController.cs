using Book_Sphere.Models;
using Book_Sphere.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book_Sphere.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;


        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;

        }
        public IActionResult Role()
        {
            return View();
        }
        public async Task<IActionResult> AddRole(RoleViewModel roleVM)
        {
            IdentityRole identityRole = new IdentityRole();
            identityRole.Name = roleVM.RoleName;

            IdentityResult result = await roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                ViewBag.Success = true;
                return View("Role");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View("Role", roleVM);
        }


        [HttpGet]
        public IActionResult RoleToExsitingUser()
        {
            var model = new AddRoleForExisingUserViewModel
            {
                RolesList = roleManager.Roles
                    .Select(r => new SelectListItem { Value = r.Name, Text = r.Name })
                    .ToList()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RoleToExsitingUser(AddRoleForExisingUserViewModel model)
        {
            // Repopulate RolesList
            model.RolesList = roleManager.Roles
                .Select(r => new SelectListItem { Value = r.Name, Text = r.Name })
                .ToList();

            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "User not found.");
                return View(model);
            }

            if (!await roleManager.RoleExistsAsync(model.RoleName))
            {
                ModelState.AddModelError("RoleName", "This role does not exist.");
                return View(model);
            }

            if (await userManager.IsInRoleAsync(user, model.RoleName))
            {
                ModelState.AddModelError("RoleName", "User is already in this role.");
                return View(model);
            }

            var result = await userManager.AddToRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }

            TempData["Success"] = $"Role '{model.RoleName}' assigned to {model.Email} successfully.";
            return RedirectToAction("RoleToExsitingUser");
        }


    }
}
