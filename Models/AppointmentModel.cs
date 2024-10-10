using System.ComponentModel.DataAnnotations;
using TLCOnline.Resources;

namespace TLCOnline.Models
{
    public class AppointmentModel
    {
        public virtual Guid AppointId { get; set; }

        [Required]
        [Display(Name = nameof(Labels.CustMyKad), ResourceType = typeof(Labels))]
        public virtual string CustMyKad { get; set; }

        [Required]
        [Display(Name = nameof(Labels.LaserName), ResourceType = typeof(Labels))]
        public virtual string LaserName { get; set; }

        [Display(Name = nameof(Labels.LaserPrice), ResourceType = typeof(Labels))]
        public virtual decimal LaserPrice { get; set; }

        [Required]
        [Display(Name = nameof(Labels.StaffName), ResourceType = typeof(Labels))]
        public virtual string StaffId { get; set; }

        [Required]
        [Display(Name = nameof(Labels.AppointDate), ResourceType = typeof(Labels))]
        public virtual DateTime AppointDate { get; set; }

        [Required]
        [Display(Name = nameof(Labels.AppointTime), ResourceType = typeof(Labels))]
        public virtual string AppointTime { get; set; }

        [Display(Name = nameof(Labels.Status), ResourceType = typeof(Labels))]
        public virtual string Status { get; set; } = Labels.Scheduled;
    }
}