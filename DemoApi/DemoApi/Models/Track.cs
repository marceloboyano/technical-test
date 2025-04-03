using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoApi.Models
{
    public class Track
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrackId { get; set; }

        [Required(ErrorMessage = "Track name is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 200 characters")]
        [Display(Name = "Track name")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Album ID is required")]
        [ForeignKey("Album")]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Media type ID is required")]
        [ForeignKey("MediaType")]
        public int MediaTypeId { get; set; }

        [Required(ErrorMessage = "Genre ID is required")]
        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        [StringLength(220, ErrorMessage = "Composer name cannot exceed 220 characters")]
        [Display(Name = "Composer")]
        public required string Composer { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be positive")]
        [Display(Name = "Duration (ms)")]
        public int Milliseconds { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "File size must be positive")]
        [Display(Name = "File size (bytes)")]
        public int Bytes { get; set; }

        [Range(0.0, 1000.0, ErrorMessage = "Price must be between $0 and $1000")] 
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }
    }
}
