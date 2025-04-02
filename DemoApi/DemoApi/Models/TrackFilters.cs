namespace DemoApi.Models
{
    public class TrackFilters : PaginationParams
    {
        public string? TrackName { get; set; }
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public string? Genre { get; set; }
    }
}
