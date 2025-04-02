using System.Text.Json.Serialization;

namespace DemoApi.Models.DTOs
{
    public class TrackSearchResultDto
    {
        public int TrackId { get; set; }
        public required string Name { get; set; }
        public required string AlbumTitle { get; set; }      
        public required string ArtistName { get; set; }
        public required string GenreName { get; set; }
        public required string Composer { get; set; }
        public int DurationMs { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public int TotalCount { get; set; }
      
    }
}
