using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoApi.Models
{
    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Album title is required")]
        [StringLength(160, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 160 characters")]
        [RegularExpression(@"^[\w\sáéíóúÁÉÍÓÚñÑüÜ\-\'\.&,!?()""]+$",
            ErrorMessage = "Contains invalid characters. Only letters, numbers, and common punctuation are allowed")]
        [Display(Name = "Album title")]
        public required string Title { get; set; }
    }
}
