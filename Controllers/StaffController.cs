using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TLCOnline.Interfaces;
using TLCOnline.Models;
using TLCOnline.Resources;

namespace TLCOnline.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly UserManager<IdentityUser> _userManager;

        public StaffController(IStaffService staffService, UserManager<IdentityUser> userManager)
        {
            _staffService = staffService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            var staffs = _staffService.GetAllStaffs();
            return View(staffs);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(StaffModel model)
        {
            if (ModelState.IsValid)
            {
                var exists = _staffService.CheckIfStaffExists(model.StaffId, model.MyKad, model.Email);
                if (exists)
                {
                    ViewData["AlertMessage"] = Labels.StaffIdOrMyKadAlreadyExists;
                    ViewData["AlertTitle"] = Labels.AddNewStaffFailed;
                    return View(model);
                }

                if (_staffService.IsHireDateInFuture(model.HireDate))
                {
                    ViewData["AlertMessage"] = Labels.HireDateCannotInFuture;
                    ViewData["AlertTitle"] = Labels.AddNewStaffFailed;
                    return View(model);
                }

                var user = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (model.Position == "Doctor")
                        await _userManager.AddToRoleAsync(user, "Doctor");
                    else if (model.Position == "Aesthetician")
                        await _userManager.AddToRoleAsync(user, "Aesthetician");
                    else if (model.Position == "Receptionist")
                        await _userManager.AddToRoleAsync(user, "Receptionist");

                    await _staffService.AddStaffAsync(model);
                    return RedirectToAction(nameof(List));
                }
                else
                {
                    ViewData["AlertMessage"] = Labels.AddNewStaffFailed;
                    ViewData["AlertTitle"] = Labels.AddNewStaff;
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            var staff = _staffService.GetStaffById(id);
            return View(staff);
        }

        [HttpPost]
        public async Task<IActionResult> Update(StaffModel model)
        {
            ModelState.Remove(nameof(model.Password));
            ModelState.Remove(nameof(model.ConfirmPassword));

            if (ModelState.IsValid)
            {
                var exists = _staffService.CheckIfEmailOrMyKadExists(model.Email, model.MyKad, model.StaffId);
                if (exists)
                {
                    ViewData["AlertMessage"] = Labels.StaffEmailAlreadyExists;
                    ViewData["AlertTitle"] = Labels.UpdateStaffFailed;
                    return View(model);
                }

                var result = await _staffService.EditStaff(model);

                if (result.IsSuccessful)
                    return RedirectToAction(nameof(List));
                else
                {
                    ViewData["AlertMessage"] = result.Message;
                    ViewData["AlertTitle"] = "Error";
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult StaffView(string id)
        {
            var staff = _staffService.GetStaffById(id);
            return View(staff);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _staffService.DeleteStaff(id);
            return RedirectToAction(nameof(List));
        }
    }
}
