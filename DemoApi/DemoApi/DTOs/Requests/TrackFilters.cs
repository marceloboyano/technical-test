using System.ComponentModel.DataAnnotations;

namespace DemoApi.DTOs.Requests
{
    public class TrackFilters : PaginationParams
    {
        [StringLength(100, ErrorMessage = "Track name cannot exceed 100 characters")]       
        public string? TrackName { get; set; }

        [StringLength(100, ErrorMessage = "Artist name cannot exceed 100 characters")]       
        public string? Artist { get; set; }

        [StringLength(100, ErrorMessage = "Album name cannot exceed 100 characters")]      
        public string? Album { get; set; }

        [StringLength(50, ErrorMessage = "Genre cannot exceed 50 characters")]       
        public string? Genre { get; set; }
    }
}
