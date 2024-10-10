using NHibernate;
using TLCOnline.Models;
using TLCOnline.Resources;

namespace TLCOnline.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ISessionFactory _sessionFactory;

        public AppointmentService(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public List<AppointmentModel> GetAllAppointments()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<AppointmentModel>()
                    .Select(a => new AppointmentModel
                    {
                        AppointId = a.AppointId,
                        AppointDate = a.AppointDate,
                        AppointTime = a.AppointTime,
                        CustMyKad = session.Query<CustomerModel>()
                            .Where(c => c.CustMyKad == a.CustMyKad)
                            .Select(c => c.CustName)
                            .SingleOrDefault(),
                        LaserName = a.LaserName,
                        LaserPrice = a.LaserPrice,
                        StaffId = session.Query<StaffModel>()
                            .Where(s => s.StaffId == a.StaffId)
                            .Select(s => s.StaffName)
                            .SingleOrDefault(),
                        Status = a.Status }).OrderByDescending(a => a.AppointDate).ToList();
            }
        }

        public ErrorMessageModel CheckAppointmentExists(DateTime appointDate, string appointTime, string custMyKad, string staffId)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var existingAppointments = session.Query<AppointmentModel>()
                    .Where(a => a.AppointDate == appointDate
                         && a.AppointTime == appointTime
                         && a.Status != Labels.Cancelled).ToList();
                
                if (existingAppointments.Count >= 3)
                    return new ErrorMessageModel { IsSuccessful = true, Message = Labels.AppointExceeded3 };

                var isCustomerExisting = existingAppointments.Any(a => a.CustMyKad == custMyKad);
                var isStaffExisting = existingAppointments.Any(a => a.StaffId == staffId);

                if (isCustomerExisting && isStaffExisting)
                    return new ErrorMessageModel { IsSuccessful = true, Message = Labels.AppointAlreadyExistsForBothCustomerAndStaff };

                if (isCustomerExisting)
                    return new ErrorMessageModel { IsSuccessful = true, Message = Labels.AppointAlreadyExistsForThatCust };

                if (isStaffExisting)
                    return new ErrorMessageModel { IsSuccessful = true, Message = Labels.AppointAlreadyExistsForThatStaff };

                return new ErrorMessageModel { IsSuccessful = false };
            }
        }

        public void AddAppointment(AppointmentModel model)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(model);
                    transaction.Commit();
                }
            }
        }

        public AppointmentModel GetAppointmentById(Guid id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Get<AppointmentModel>(id);
            }
        }

        public void UpdateAppointment(AppointmentModel model)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var updateAppoint = session.Get<AppointmentModel>(model.AppointId);
                    updateAppoint.LaserName = model.LaserName;
                    updateAppoint.LaserPrice = model.LaserPrice;
                    updateAppoint.StaffId = model.StaffId;
                    updateAppoint.AppointDate = model.AppointDate;
                    updateAppoint.AppointTime = model.AppointTime;
                    updateAppoint.Status = model.Status;

                    session.Update(updateAppoint);
                    transaction.Commit();
                }
            }
        }

        public void DeleteAppointment(Guid id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var appointment = session.Get<AppointmentModel>(id);
                    session.Delete(appointment);
                    transaction.Commit();
                }
            }
        }
    }
}