using DemoApi.DTOs.Responses;

namespace DemoApi.Services.Interfaces
{
    /// <summary>
    /// Service implementation for artist-related operations.
    /// </summary>
    public interface IArtistsService
    {
        /// <summary>
        /// Retrieves all available artists, sorted by name in ascending order.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A list of artists.</returns>
        Task<IEnumerable<ArtistResponseDto>> GetAllArtistsAsync(CancellationToken cancellationToken);
    }
}
