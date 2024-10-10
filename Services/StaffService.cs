using Microsoft.AspNetCore.Identity;
using NHibernate;
using TLCOnline.Interfaces;
using TLCOnline.Models;
using TLCOnline.Resources;

namespace TLCOnline.Services
{
    public class StaffService : IStaffService
    {
        private readonly ISessionFactory _sessionFactory;

        public StaffService(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public List<StaffModel> GetAllStaffs()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<StaffModel>().OrderByDescending(a => a.HireDate).ToList();
            }
        }

        public async Task<ErrorMessageModel> EditStaff(StaffModel model)
        {
            bool isUnique = CheckIfEmailOrMyKadExists(model.Email, model.MyKad, model.StaffId);

            if(isUnique)
                return new ErrorMessageModel { IsSuccessful = false, Message = Labels.StaffIdOrMyKadAlreadyExists };

            try
            {
                UpdateStaff(model);
                return new ErrorMessageModel { IsSuccessful = true };
            }
            catch (Exception ex)
            {
                return new ErrorMessageModel { IsSuccessful = false, Message = ex.Message };
            }
        }

        public async Task AddStaffAsync(StaffModel model)
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

        public StaffModel GetStaffById(string id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Get<StaffModel>(id);
            }
        }

        public void UpdateStaff(StaffModel model)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var updateStaff = session.Get<StaffModel>(model.StaffId);

                    updateStaff.Position = model.Position;
                    updateStaff.MyKad = model.MyKad;
                    updateStaff.Email = model.Email;
                    updateStaff.Gender = model.Gender;
                    updateStaff.BirthDate = model.BirthDate;
                    updateStaff.StaffName = model.StaffName;
                    updateStaff.PhoneNo = model.PhoneNo;
                    updateStaff.Status = model.Status;

                    session.Update(updateStaff);
                    transaction.Commit();
                }
            }
        }

        public void DeleteStaff(string id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var staff = session.Get<StaffModel>(id);
                    var appointments = session.Query<AppointmentModel>().Where(a => a.StaffId == id).ToList();

                    foreach (var appointment in appointments)
                    {
                        session.Delete(appointment);
                    }

                    session.Delete(staff);
                    transaction.Commit();
                }
            }
        }

        public bool CheckIfStaffExists(string staffId, string myKad, string email)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<StaffModel>().Any(a => a.StaffId == staffId || a.MyKad == myKad || a.Email == email);
            }
        }

        public bool CheckIfEmailOrMyKadExists(string email, string myKad, string staffId)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<StaffModel>().Any(a => (a.Email == email || a.MyKad == myKad) && a.StaffId != staffId);
            }
        }

        public bool IsHireDateInFuture(DateTime hireDate)
        {
            return hireDate.Date > DateTime.Now.Date;
        }
    }
}