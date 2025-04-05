using DemoApi.DTOs.Requests;
using DemoApi.DTOs.Responses;
using DemoApi.Models;

namespace DemoApi.Repositories.Interfaces
{
    public interface ITracksRepository : IGenericRepository<Track>
    {
        /// <summary>
        /// Searches tracks based on specified filters and returns paginated results with caching support.
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedResponse<TrackSearchResultDto>> SearchTracksAsync(TrackFilters filters, CancellationToken cancellationToken);
    }
}
