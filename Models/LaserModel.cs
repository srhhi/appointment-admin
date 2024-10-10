using System.ComponentModel.DataAnnotations;
using TLCOnline.Resources;

namespace TLCOnline.Models
{
    public class LaserModel
    {
        [Required]
        [Display(Name = nameof(Labels.LaserName), ResourceType = typeof(Labels))]
        public virtual string LaserName { get; set; }

        [Required]
        [Display(Name = nameof(Labels.Description), ResourceType = typeof(Labels))]
        public virtual string LaserDesc { get; set; }

        [Required]
        [Display(Name = nameof(Labels.NormalPrice), ResourceType = typeof(Labels))]
        public virtual decimal NormalPrice { get; set; }

        [Required]
        [Display(Name = nameof(Labels.Discount), ResourceType = typeof(Labels))]
        public virtual decimal Discount { get; set; }

        [Display(Name = nameof(Labels.LaserPrice), ResourceType = typeof(Labels))]
        public virtual decimal LaserPrice { get; set; } //auto
    }
}