using NHibernate;
using TLCOnline.Interfaces;
using TLCOnline.Models;

namespace TLCOnline.Services
{
    public class LaserService : ILaserService
    {
        private readonly ISessionFactory _sessionFactory;

        public LaserService(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public List<LaserModel> GetAllLasers()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<LaserModel>().ToList();
            }
        }

        public void AddLaser(LaserModel model)
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

        public LaserModel GetLaserById(string id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Get<LaserModel>(id);
            }
        }

        public void UpdateLaser(LaserModel model)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var updateLaser = session.Get<LaserModel>(model.LaserName);
                    updateLaser.LaserDesc = model.LaserDesc;
                    updateLaser.NormalPrice = model.NormalPrice;
                    updateLaser.Discount = model.Discount;
                    updateLaser.LaserPrice = model.LaserPrice;

                    session.Update(updateLaser);
                    transaction.Commit();
                }
            }
        }

        public void DeleteLaser(string id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var laser = session.Get<LaserModel>(id);
                    var appointments = session.Query<AppointmentModel>().Where(a => a.LaserName == id).ToList();

                    if (appointments.Any())
                    {
                        foreach (var appointment in appointments)
                        {
                            session.Delete(appointment);
                        }
                    }

                    session.Delete(laser);
                    transaction.Commit();
                }
            }
        }

        public bool CheckIfLaserNameExists(string laserName)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<LaserModel>().Any(a => a.LaserName == laserName);
            }
        }
    }
}