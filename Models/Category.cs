using Microsoft.EntityFrameworkCore;

namespace Book_Sphere.Models
{
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Url(ErrorMessage = "Please enter a valid image URL")]
        public string? ImageUrl { get; set; }

        [ValidateNever]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public List<Book>? Books { get; set; }
    }

}
