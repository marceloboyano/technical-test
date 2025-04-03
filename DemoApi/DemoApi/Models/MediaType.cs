using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoApi.Models
{
    public class MediaType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MediaTypeId { get; set; }

        [Required(ErrorMessage = "Media type name is required")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 120 characters")]
        [RegularExpression(@"^[a-zA-Z\s\-]+$",
            ErrorMessage = "Only letters, spaces and hyphens are allowed")]
        [Display(Name = "Media type name")]
        public required string Name { get; set; }
    }
}
