using System.ComponentModel.DataAnnotations;

namespace CleverCore.WebApp.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember")]
        public bool RememberMe { get; set; }
    }
}
