using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHibernate;
using TLCOnline.Models;
using TLCOnline.Resources;
using TLCOnline.Services;

namespace TLCOnline.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ISessionFactory _sessionFactory;

        public AppointmentController(ISessionFactory sessionFactory, IAppointmentService appointmentService)
        {
            _sessionFactory = sessionFactory;
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var appointments = _appointmentService.GetAllAppointments();
            return View(appointments);
        }

        [HttpPost]
        public JsonResult CheckAppointmentExists(DateTime appointDate, string appointTime, string custMyKad, string staffId)
        {
            // Ensure the appointment date is at least one day in the future
            var currentDateTime = DateTime.Now;
            if (appointDate.Date <= currentDateTime.Date.AddDays(1))
                return Json(new { exists = true, error = Labels.AppointDateMustBeAtLeast1DayInTheFuture });

            // Ensure the appointment date is not on a weekend
            if (appointDate.DayOfWeek == DayOfWeek.Saturday || appointDate.DayOfWeek == DayOfWeek.Sunday)
                return Json(new { exists = true, error = Labels.AppointDateCannotOnWeekend });

            // Check if an appointment exists for the given date and time
            var result = _appointmentService.CheckAppointmentExists(appointDate, appointTime, custMyKad, staffId);

            if (result.IsSuccessful)
                return Json(new { exists = true, error = result.Message });

            return Json(new { exists = false });
        }

        [HttpGet]
        public IActionResult GetLaserPrice(string LaserName)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var laser = session.Get<LaserModel>(LaserName);
                var price = laser?.LaserPrice ?? 0;
                return Json(new { price });
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                ViewBag.Customers = session.Query<CustomerModel>().Select(c => new SelectListItem
                {
                    Value = c.CustMyKad.ToString(),
                    Text = c.CustName }).ToList();

                ViewBag.Lasers = session.Query<LaserModel>().Select(l => new SelectListItem
                {
                    Value = l.LaserName.ToString(),
                    Text = l.LaserName }).ToList();

                ViewBag.Staffs = session.Query<StaffModel>().Where(s => s.Status != Labels.Inactive).Select(s => new SelectListItem
                {
                    Value = s.StaffId.ToString(),
                    Text = s.StaffName }).ToList();

                return View();
            }
        }

        [HttpPost]
        public IActionResult Add(AppointmentModel model)
        {
            if (ModelState.IsValid)
            {
                _appointmentService.AddAppointment(model);
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                ViewBag.Lasers = session.Query<LaserModel>().Select(l => new SelectListItem
                {
                    Value = l.LaserName.ToString(),
                    Text = l.LaserName }).ToList();

                ViewBag.Staffs = session.Query<StaffModel>().Where(s => s.Status != Labels.Inactive).Select(s => new SelectListItem
                {
                    Value = s.StaffId.ToString(),
                    Text = s.StaffName }).ToList();

                var appointment = _appointmentService.GetAppointmentById(id);
                return View(appointment);
            }
        }

        [HttpPost]
        public IActionResult Update(AppointmentModel model)
        {
            if (ModelState.IsValid)
            {
                _appointmentService.UpdateAppointment(model);
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AppointmentView(Guid id)
        {
            var appointment = _appointmentService.GetAppointmentById(id);
            return View(appointment);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _appointmentService.DeleteAppointment(id);
            return RedirectToAction(nameof(List));
        }
    }
}