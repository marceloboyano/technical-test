using DemoApi.DTOs.Responses;

namespace DemoApi.Services.Interfaces
{
    /// <summary>
    /// Service for handling operations related to music genres.
    /// </summary>
    public interface IGenresService
    {
        /// <summary>
        /// Retrieves all music genres sorted by name in ascending order (A-Z).
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A collection of <see cref="GenreResponseDto"/> objects representing the genres.</returns>
        Task<IEnumerable<GenreResponseDto>> GetAllGenresAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves the number of songs per music genre.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A collection of <see cref="GenreSummaryResponseDto"/> objects containing genre names and song counts.</returns>
        Task<IEnumerable<GenreSummaryResponseDto>> GetSongCountsPerGenreAsync(CancellationToken cancellationToken);
    }
}
