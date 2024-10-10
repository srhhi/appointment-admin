namespace TLCOnline.Models
{
    public class HomeModel
    {
        public int TotalAppointments { get; set; }
        public int TotalStaff { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalLasers { get; set; }
        public List<UpcomingAppointmentModel> UpcomingAppointments { get; set; }

        //chart data
        public int TotalScheduled { get; set; }
        public int TotalCompleted { get; set; }
        public int TotalCancelled { get; set; }
        public string[] AppointmentsByStatusLabels { get; set; }
        public int[] AppointmentsByStatusValues { get; set; }
        public string[] AppointmentsByLaserLabels { get; set; }
        public int[] AppointmentsByLaserValues { get; set; }
        public string[] CustomersByGenderLabels { get; set; }
        public int[] CustomersByGenderValues { get; set; }
    }

    public class UpcomingAppointmentModel
    {
        public Guid AppointId { get; set; }
        public string CustomerName { get; set; }
        public string LaserName { get; set; }
        public string StaffName { get; set; }
        public DateTime AppointDate { get; set; }
        public string AppointTime { get; set; }
        public string Status { get; set; }
    }
}