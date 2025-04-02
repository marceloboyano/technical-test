namespace DemoApi.Models.DTOs
{
    public class TrackResponseDto
    {
        public int TrackId { get; set; }
        public required string Name { get; set; }
        public required string Composer { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
