using DemoApi.DTOs.Requests;
using DemoApi.DTOs.Responses;

namespace DemoApi.Services.Interfaces
{
    public interface ITracksService
    {
        Task<TrackResponseDto?> GetTrackByIdAsync(int trackId);
        Task<PagedResponse<TrackSearchResultDto>> SearchTracksAsync(TrackFilters filters);
    }
}
