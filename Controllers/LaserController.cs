using Microsoft.AspNetCore.Mvc;
using TLCOnline.Interfaces;
using TLCOnline.Models;
using TLCOnline.Resources;

namespace TLCOnline.Controllers
{
    public class LaserController : Controller
    {
        private readonly ILaserService _laserService;

        public LaserController(ILaserService laserService)
        {
            _laserService = laserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            var lasers = _laserService.GetAllLasers();
            return View(lasers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(LaserModel model)
        {
            if (ModelState.IsValid)
            {
                var exists = _laserService.CheckIfLaserNameExists(model.LaserName);
                if (exists)
                {
                    ViewData["AlertMessage"] = Labels.LaserNameAlreadyExists;
                    ViewData["AlertTitle"] = Labels.AddNewLaserFailed;
                    return View(model);
                }

                _laserService.AddLaser(model);
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            var laser = _laserService.GetLaserById(id);
            return View(laser);
        }

        [HttpPost]
        public IActionResult Update(LaserModel model)
        {
            if (ModelState.IsValid)
            {
                _laserService.UpdateLaser(model);
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult LaserView(string id)
        {
            var laser = _laserService.GetLaserById(id);
            return View(laser);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _laserService.DeleteLaser(id);
            return Json(new { success = true });
        }
    }
}
