using TLCOnline.Models;

namespace TLCOnline.Interfaces
{
    public interface IStaffService
    {
        List<StaffModel> GetAllStaffs();
        Task AddStaffAsync(StaffModel model);
        StaffModel GetStaffById(string id);
        void UpdateStaff(StaffModel model);
        void DeleteStaff(string id);
        bool CheckIfStaffExists(string staffId, string myKad, string email);
        bool CheckIfEmailOrMyKadExists(string email, string myKad, string staffId);
        bool IsHireDateInFuture(DateTime hireDate);
        Task<ErrorMessageModel> EditStaff(StaffModel model);
    }
}