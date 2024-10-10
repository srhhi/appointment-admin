using System.ComponentModel.DataAnnotations;
using TLCOnline.Resources;

namespace TLCOnline.Models
{
    public class CustomerModel
    {
        [Required]
        [Display(Name = nameof(Labels.CustMyKad), ResourceType = typeof(Labels))]
        [StringLength(12, ErrorMessageResourceName = nameof(Messages.MyKadMustBeExactly12Digits), ErrorMessageResourceType = typeof(Messages))]
        [RegularExpression(@"^\d{12}$", ErrorMessageResourceName = nameof(Messages.MyKadMustBeExactly12Digits), ErrorMessageResourceType = typeof(Messages))]
        public virtual string CustMyKad { get; set; }

        [Required]
        [Display(Name = nameof(Labels.CustEmail), ResourceType = typeof(Labels))]
        [EmailAddress(ErrorMessageResourceName = nameof(Messages.InvalidEmailAddress), ErrorMessageResourceType = typeof(Messages))]
        public virtual string Email { get; set; }

        [Required]
        [Display(Name = nameof(Labels.CustName), ResourceType = typeof(Labels))]
        public virtual string CustName { get; set; }

        [Display(Name = nameof(Labels.Gender), ResourceType = typeof(Labels))]
        public virtual string Gender { get; set; } //auto

        [Required]
        [Display(Name = nameof(Labels.PhoneNo), ResourceType = typeof(Labels))]
        [RegularExpression(@"^\d{10,12}$", ErrorMessageResourceName = nameof(Messages.PhoneNoMustBeBetween10And12Digits), ErrorMessageResourceType = typeof(Messages))]
        public virtual string PhoneNo { get; set; }

        [Display(Name = nameof(Labels.BirthDate), ResourceType = typeof(Labels))]
        public virtual DateTime BirthDate { get; set; } //auto

        [Display(Name = nameof(Labels.RegisterDate), ResourceType = typeof(Labels))]
        public virtual DateTime RegisterDate { get; set; } = DateTime.Now; //auto
    }
}