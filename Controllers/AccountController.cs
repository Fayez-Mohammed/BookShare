using Book_Sphere.Models;
using Book_Sphere.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Book_Sphere.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["HideFooter"] = true;
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveRegister(RegisterViewModel registerVM)
        {
            ViewData["HideFooter"] = true;
            ApplicationUser user = new ApplicationUser();
            user.UserName = registerVM.FullName;
            user.Email = registerVM.Email;
            user.PasswordHash = registerVM.Password;
            IdentityResult result = await userManager.CreateAsync(user, registerVM.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }
            return View("Register", registerVM);
        }
        public IActionResult Login()
        {
            ViewData["HideFooter"] = true;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveLogin(LoginViewModel loginVM)
        {
            ViewData["HideFooter"] = true;
            if (ModelState.IsValid)
            {
                ApplicationUser? user = await userManager.FindByEmailAsync(loginVM.Email);
                if (user != null)
                {
                    bool IsRight = await userManager.CheckPasswordAsync(user, loginVM.Password);
                    if (IsRight)
                    {
                        await signInManager.SignInAsync(user, loginVM.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Email or Password are Wrong");
            }
            return View("Login", loginVM);

        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
