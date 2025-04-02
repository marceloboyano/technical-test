namespace DemoApi.Models
{
    public class GenreSummary
    {
        public int GenreId { get; set; }
        public required string Name { get; set; }
        public int SongCount { get; set; }
    }
}
