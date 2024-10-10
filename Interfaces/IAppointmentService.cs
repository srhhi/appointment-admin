using TLCOnline.Models;

namespace TLCOnline.Services
{
    public interface IAppointmentService
    {
        List<AppointmentModel> GetAllAppointments();
        ErrorMessageModel CheckAppointmentExists(DateTime appointDate, string appointTime, string custMyKad, string staffId);
        void AddAppointment(AppointmentModel model);
        AppointmentModel GetAppointmentById(Guid id);
        void UpdateAppointment(AppointmentModel model);
        void DeleteAppointment(Guid id);
    }
}