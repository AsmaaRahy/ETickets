using ETickets.Models;
using ETickets.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETickets.Controllers
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
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(ApplicationUserVM userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = userVM.Name,
                    Email = userVM.Email,
                    PasswordHash = userVM.Password,
                };

                var result = await userManager.CreateAsync(applicationUser, userVM.Password);
                await userManager.AddToRoleAsync(applicationUser,"User");

                if (result.Succeeded)
                {
                    //isPersistent --> remember me btthtafez bel data l 15 day f cookie
                    await signInManager.SignInAsync(applicationUser, true);
                    return RedirectToAction("Index", "Home");

                }
                return View();

            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM userLoginVM)
        {
            if (ModelState.IsValid) {
                var user=await userManager.FindByNameAsync(userLoginVM.UserName);
                if (user != null) { 
                    var check= await userManager.CheckPasswordAsync(user,userLoginVM.Password);

                    if (check)
                    {
                        await signInManager.SignInAsync(user, userLoginVM.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("InvalidPassword", "Invalid Password");
                }
                else
                {
                    ModelState.AddModelError("invalidName", "Invalid Name");
                }

                
            }
            return View(userLoginVM);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        



    }
}
