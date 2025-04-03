using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoApi.Models
{
    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArtistId { get; set; }

        [Required(ErrorMessage = "Artist name is required")]
        [StringLength(120, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 120 characters")]
        [RegularExpression(@"^[a-zA-Z\sáéíóúÁÉÍÓÚñÑüÜ\-\'\.&]+$",
            ErrorMessage = "Only letters, spaces, hyphens, apostrophes, periods and ampersands are allowed")]
        [Display(Name = "Artist name")]
        public required string Name { get; set; }
    }
}
