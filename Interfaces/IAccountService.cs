using TLCOnline.Models;

namespace TLCOnline.Interfaces
{
    public interface IAccountService
    {
        Task<ErrorMessageModel> ValidateUserAsync(string email, string password);
        StaffModel GetStaffByEmail(string email);
        Task UpdateStaffAsync(StaffModel model);
    }
}