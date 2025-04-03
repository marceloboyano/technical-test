using DemoApi.DTOs.Requests;
using DemoApi.DTOs.Responses;
using DemoApi.Models;

namespace DemoApi.Repositories.Interfaces
{
    public interface ITracksRepository : IGenericRepository<Track>
    {
        Task<PagedResponse<TrackSearchResultDto>> SearchTracksAsync(TrackFilters filters,CancellationToken cancellationToken);
    }
}
