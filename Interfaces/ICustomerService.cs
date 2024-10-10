using TLCOnline.Models;

namespace TLCOnline.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerModel> GetAllCustomers();
        void AddCustomer(CustomerModel model);
        CustomerModel GetCustomerById(string id);
        void UpdateCustomer(CustomerModel model);
        void DeleteCustomer(string id);
        bool CheckIfCustomerExists(string myKad, string email);
        bool CheckIfEmailExists(string email, string myKad);
    }
}