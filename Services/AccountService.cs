using Microsoft.AspNetCore.Identity;
using TLCOnline.Interfaces;
using TLCOnline.Models;
using TLCOnline.Resources;

namespace TLCOnline.Services
{
    public class AccountService : IAccountService
    {
        private readonly NHibernate.ISession _session;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountService(NHibernate.ISession session, UserManager<IdentityUser> userManager)
        {
            _session = session;
            _userManager = userManager;
        }

        public StaffModel GetStaffByEmail(string email)
        {
            return _session.Query<StaffModel>().SingleOrDefault(s => s.Email == email);
        }

        public async Task<ErrorMessageModel> ValidateUserAsync(string email, string password)
        {
            var staff = _session.Query<StaffModel>().SingleOrDefault(s => s.Email == email);

            if (staff != null && staff.Status == "Active")
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null && await _userManager.CheckPasswordAsync(user, password))
                {
                    return new ErrorMessageModel { IsSuccessful = true };
                }
            }
            else if (staff != null && staff.Status == "Inactive")
            {
                return new ErrorMessageModel { IsSuccessful = false, Message = Labels.InactiveAccountPleaseContactAdmin };
            }

            return new ErrorMessageModel { IsSuccessful = false, Message = Labels.IncorrectEmailPassword };
        }

        public async Task UpdateStaffAsync(StaffModel model)
        {
            using (var transaction = _session.BeginTransaction())
            {
                var existingStaff = _session.Get<StaffModel>(model.StaffId);

                if (existingStaff != null)
                {
                    existingStaff.PhoneNo = model.PhoneNo;
                    existingStaff.Password = model.Password;

                    var user = await _userManager.FindByEmailAsync(existingStaff.Email);
                    if (user != null)
                    {
                        user.Email = model.Email; // Update email in Identity
                        if (!string.IsNullOrWhiteSpace(model.Password))
                        {
                            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                        }

                        var updateResult = await _userManager.UpdateAsync(user);
                    }
                    _session.Update(existingStaff);
                    transaction.Commit();
                }
            }
        }
    }
}