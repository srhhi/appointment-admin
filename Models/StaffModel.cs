using System.ComponentModel.DataAnnotations;
using TLCOnline.Resources;

namespace TLCOnline.Models
{
    public class StaffModel
    {
        [Required]
        [Display(Name = nameof(Labels.StaffId), ResourceType = typeof(Labels))]
        public virtual string StaffId { get; set; }

        [Required]
        [Display(Name = nameof(Labels.StaffPosition), ResourceType = typeof(Labels))]
        public virtual string Position { get; set; }

        [Required]
        [Display(Name = nameof(Labels.StaffMyKad), ResourceType = typeof(Labels))]
        [StringLength(12, ErrorMessageResourceName = nameof(Messages.MyKadMustBeExactly12Digits), ErrorMessageResourceType = typeof(Messages))]
        [RegularExpression(@"^\d{12}$", ErrorMessageResourceName = nameof(Messages.MyKadMustBeExactly12Digits), ErrorMessageResourceType = typeof(Messages))]
        public virtual string MyKad { get; set; }

        [Required]
        [Display(Name = nameof(Labels.StaffEmail), ResourceType = typeof(Labels))]
        [EmailAddress(ErrorMessageResourceName = nameof(Messages.InvalidEmailAddress), ErrorMessageResourceType = typeof(Messages))]
        public virtual string Email { get; set; }

        [Required]
        [Display(Name = nameof(Labels.StaffName), ResourceType = typeof(Labels))]
        public virtual string StaffName { get; set; }
        public virtual string Gender { get; set; } //auto

        [Required]
        [Display(Name = nameof(Labels.StaffPhoneNo), ResourceType = typeof(Labels))]
        [RegularExpression(@"^\d{10,12}$", ErrorMessageResourceName = nameof(Messages.PhoneNoMustBeBetween10And12Digits), ErrorMessageResourceType = typeof(Messages))]
        public virtual string PhoneNo { get; set; }

        [Display(Name = nameof(Labels.BirthDate), ResourceType = typeof(Labels))]
        public virtual DateTime BirthDate { get; set; } //auto

        [Display(Name = nameof(Labels.RegisterDate), ResourceType = typeof(Labels))]
        public virtual DateTime RegisterDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = nameof(Labels.HireDate), ResourceType = typeof(Labels))]
        public virtual DateTime HireDate { get; set; }

        [Required]
        [Display(Name = nameof(Labels.Password), ResourceType = typeof(Labels))]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessageResourceName = nameof(Messages.PasswordMustContainUppercaseLetterLowercaseLetterAndNumber), ErrorMessageResourceType = typeof(Messages))]
        public virtual string Password { get; set; }

        [Required]
        [Display(Name = nameof(Labels.ConfirmPassword), ResourceType = typeof(Labels))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(Messages.ConfirmPasswordShouldMatchPassword), ErrorMessageResourceType = typeof(Messages))]
        public virtual string ConfirmPassword { get; set; }

        [Display(Name = nameof(Labels.Status), ResourceType = typeof(Labels))]
        public virtual string Status { get; set; } = Labels.Active;
    }
}