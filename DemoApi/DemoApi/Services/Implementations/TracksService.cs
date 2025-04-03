using AutoMapper;
using DemoApi.DTOs.Requests;
using DemoApi.DTOs.Responses;
using DemoApi.Repositories.Interfaces;
using DemoApi.Services.Interfaces;

namespace DemoApi.Services.Implementations
{
    public class TracksService : ITracksService
    {
        private readonly ITracksRepository _repo;
        private readonly IMapper _mapper;

        public TracksService(ITracksRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<TrackResponseDto?> GetTrackByIdAsync(int trackId)
        {
            var track = await _repo.GetByIdAsync(trackId);
            return track == null ? null : _mapper.Map<TrackResponseDto>(track);
        }
        public async Task<PagedResponse<TrackSearchResultDto>> SearchTracksAsync(TrackFilters filters)
        {
            return await _repo.SearchTracksAsync(filters);
        }
    }
}
