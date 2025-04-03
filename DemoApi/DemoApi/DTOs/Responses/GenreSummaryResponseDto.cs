namespace DemoApi.DTOs.Responses
{
    public class GenreSummaryResponseDto
    {
        public int GenreId { get; set; }
        public required string Name { get; set; }
        public int SongCount { get; set; }
    }
}
