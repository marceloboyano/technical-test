using DemoApi.DTOs.Responses;

namespace DemoApi.Services.Interfaces
{
    public interface IGenresService
    {
        Task<IEnumerable<GenreResponseDto>> GetAllGenresAsync();
        Task<IEnumerable<GenreSummaryResponseDto>> GetSongCountsPerGenreAsync();
    }
}
