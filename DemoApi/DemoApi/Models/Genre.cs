using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoApi.Models
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Genre name is required")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 120 characters")]
        [RegularExpression(@"^[a-zA-Z\sáéíóúÁÉÍÓÚñÑüÜ\-]+$",
            ErrorMessage = "Only letters, spaces, accents and hyphens are allowed. Numbers are not permitted.")]
        [Display(Name = "Genre name")]
        public required string Name { get; set; }
    }
}
