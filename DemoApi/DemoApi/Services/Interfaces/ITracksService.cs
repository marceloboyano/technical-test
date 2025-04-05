using DemoApi.DTOs.Requests;
using DemoApi.DTOs.Responses;

namespace DemoApi.Services.Interfaces
{
    /// <summary>
    /// Service for handling track-related operations.
    /// </summary>
    public interface ITracksService
    {
        /// <summary>
        /// Retrieves a track by its unique identifier.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The track if found; otherwise, null.</returns>
        Task<TrackResponseDto?> GetTrackByIdAsync(int trackId,CancellationToken cancellationToken);
        /// <summary>
        /// Searches for tracks based on the specified filters.
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A paged response containing the matching tracks and pagination information.</returns>
        Task<PagedResponse<TrackSearchResultDto>> SearchTracksAsync(TrackFilters filters,CancellationToken cancellationToken);
    }
}
