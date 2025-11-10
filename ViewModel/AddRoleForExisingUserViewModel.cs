using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Book_Sphere.ViewModel
{
    public class AddRoleForExisingUserViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a role")]
        public string RoleName { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; } = new List<SelectListItem>();



    }
}
