using System.ComponentModel.DataAnnotations;

namespace Book_Sphere.ViewModel
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
