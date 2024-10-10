using System.ComponentModel.DataAnnotations;
using TLCOnline.Resources;

namespace TLCOnline.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceName = nameof(Messages.InvalidEmailAddress), ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(Labels.Email), ResourceType = typeof(Labels))]
        public string Email { get; set; }

        [Required]
        [Display(Name = nameof(Labels.Password), ResourceType = typeof(Labels))]
        public string Password { get; set; }
    }
}