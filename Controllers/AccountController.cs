using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TLCOnline.Interfaces;
using TLCOnline.Models;
using TLCOnline.Resources;

namespace TLCOnline.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(IAccountService accountService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _accountService = accountService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.CurrentDateTime = DateTime.Now.ToString("dddd, dd MMM yyyy h:mm:ss tt");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.ValidateUserAsync(model.Email, model.Password);
                if (result.IsSuccessful)
                {
                    var staff = _accountService.GetStaffByEmail(model.Email);
                    if (staff !=null)
                    {
                        var user = await _userManager.FindByEmailAsync(model.Email);
                        if (user != null)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                }
                ViewData["AlertMessage"] = result.Message;
                ViewData["AlertTitle"] = Labels.LoginError;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            var userEmail = User.Identity.Name;
            var staff = _accountService.GetStaffByEmail(userEmail);

            return View(staff);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(StaffModel model)
        {
            if (ModelState.IsValid)
            {
                await _accountService.UpdateStaffAsync(model);
                ViewData["AlertMessage"] = "Profile updated successfully!";
                ViewData["AlertTitle"] = "Success";
                return RedirectToAction(nameof(Profile));
            }
            return View(model);
        }
    }
}