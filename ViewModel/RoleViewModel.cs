using System.ComponentModel.DataAnnotations;

namespace Book_Sphere.ViewModel
{
    public class RoleViewModel
    {
        [Display(Name = "Role Name")]
        public required string RoleName { get; set; }
    }
}
