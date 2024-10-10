using Microsoft.AspNetCore.Mvc;
using TLCOnline.Models;
using TLCOnline.Resources;

namespace TLCOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly NHibernate.ISession _session;

        public HomeController(NHibernate.ISession session)
        {
            _session = session;
        }

        public IActionResult Index()
        {
            var model = new HomeModel
            {
                TotalAppointments = _session.Query<AppointmentModel>().Count(),
                TotalStaff = _session.Query<StaffModel>().Count(),
                TotalCustomers = _session.Query<CustomerModel>().Count(),
                TotalLasers = _session.Query<LaserModel>().Count(),

                UpcomingAppointments = _session.Query<AppointmentModel>()
                .Where(a => a.AppointDate >= DateTime.Now && a.Status == Labels.Scheduled)
                .Select(a => new UpcomingAppointmentModel
                {
                    AppointId = a.AppointId,
                    CustomerName = _session.Query<CustomerModel>()
                        .Where(c => c.CustMyKad == a.CustMyKad).Select(c => c.CustName).SingleOrDefault(),
                    LaserName = a.LaserName,
                    StaffName = _session.Query<StaffModel>()
                        .Where(s => s.StaffId == a.StaffId).Select(s => s.StaffName).SingleOrDefault(),
                    AppointDate = a.AppointDate,
                    AppointTime = a.AppointTime,
                    Status = a.Status }).OrderBy(a => a.AppointDate).ToList(),

                TotalScheduled = _session.Query<AppointmentModel>().Count(a => a.Status == Labels.Scheduled),
                TotalCompleted = _session.Query<AppointmentModel>().Count(a => a.Status == Labels.Completed),
                TotalCancelled = _session.Query<AppointmentModel>().Count(a => a.Status == Labels.Cancelled),

                AppointmentsByStatusLabels = _session.Query<AppointmentModel>()
                    .GroupBy(a => a.Status).Select(g => g.Key).ToArray(),
                AppointmentsByStatusValues = _session.Query<AppointmentModel>()
                    .GroupBy(a => a.Status).Select(g => g.Count()).ToArray(),
                AppointmentsByLaserLabels = _session.Query<AppointmentModel>()
                    .GroupBy(a => a.LaserName).Select(g => g.Key).ToArray(),
                AppointmentsByLaserValues = _session.Query<AppointmentModel>()
                    .GroupBy(a => a.LaserName).Select(g => g.Count()).ToArray(),
                CustomersByGenderLabels = _session.Query<CustomerModel>()
                    .GroupBy(c => c.Gender).Select(g => g.Key).ToArray(),
                CustomersByGenderValues = _session.Query<CustomerModel>()
                    .GroupBy(c => c.Gender).Select(g => g.Count()).ToArray()
            };
            return View(model);
        }
    }
}