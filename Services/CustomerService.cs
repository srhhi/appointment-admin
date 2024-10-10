using NHibernate;
using TLCOnline.Interfaces;
using TLCOnline.Models;

namespace TLCOnline.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ISessionFactory _sessionFactory;

        public CustomerService(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public List<CustomerModel> GetAllCustomers()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<CustomerModel>().OrderByDescending(a => a.RegisterDate).ToList();
            }
        }

        public void AddCustomer(CustomerModel model)
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

        public CustomerModel GetCustomerById(string id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Get<CustomerModel>(id);
            }
        }

        public void UpdateCustomer(CustomerModel model)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var updateCust = session.Get<CustomerModel>(model.CustMyKad);

                    if (updateCust != null) 
                    {
                        updateCust.Email = model.Email;
                        updateCust.CustName = model.CustName;
                        updateCust.PhoneNo = model.PhoneNo;

                        session.Update(updateCust);
                        transaction.Commit();
                    }
                }
            }
        }

        public void DeleteCustomer(string id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var customer = session.Get<CustomerModel>(id);
                    var appointments = session.Query<AppointmentModel>().Where(a => a.CustMyKad == id).ToList();

                    if (appointments.Any())
                    {
                        foreach (var appointment in appointments)
                        {
                            session.Delete(appointment);
                        }
                    }

                    session.Delete(customer);
                    transaction.Commit();
                }
            }
        }

        public bool CheckIfCustomerExists(string myKad, string email)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<CustomerModel>().Any(a => a.CustMyKad == myKad || a.Email == email);
            }
        }

        public bool CheckIfEmailExists(string email, string myKad)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<CustomerModel>().Any(a => a.Email == email && a.CustMyKad != myKad);
            }
        }
    }
}
