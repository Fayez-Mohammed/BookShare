using Microsoft.EntityFrameworkCore;

namespace Book_Sphere.Models
{
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Book title is required")]
        [StringLength(150, ErrorMessage = "Title cannot exceed 150 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author name is required")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters")]
        public string Auther { get; set; } = string.Empty;

        [Range(0, 5, ErrorMessage = "Stars must be between 0 and 5")]
        public int? Stars { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Url(ErrorMessage = "Please enter a valid image URL")]
        public string? ImageUrl { get; set; }

        [Url(ErrorMessage = "Please enter a valid PDF URL")]
        public string? PdfUrl { get; set; }
        public bool? Favor { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [ForeignKey(nameof(CategoryId))]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }

        [ValidateNever]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Category? Category { get; set; }
    }

}
