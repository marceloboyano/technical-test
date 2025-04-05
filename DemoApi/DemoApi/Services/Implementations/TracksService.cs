using AutoMapper;
using DemoApi.DTOs.Requests;
using DemoApi.DTOs.Responses;
using DemoApi.Repositories.Interfaces;
using DemoApi.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace DemoApi.Services.Implementations
{
    public class TracksService : ITracksService
    {
        private readonly ITracksRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public TracksService(ITracksRepository repo, IMapper mapper, IMemoryCache cache)
        {
            _repo = repo;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<TrackResponseDto?> GetTrackByIdAsync(int trackId, CancellationToken cancellationToken)
        {
            var track = await _repo.GetByIdAsync(trackId, cancellationToken);
            return track == null ? null : _mapper.Map<TrackResponseDto>(track);
        }
        public async Task<PagedResponse<TrackSearchResultDto>> SearchTracksAsync(TrackFilters filters, CancellationToken cancellationToken)
        {
            SanitizeFilters(filters);
            if (!HasValidFilters(filters))
            {
                return new PagedResponse<TrackSearchResultDto>(
                    new List<TrackSearchResultDto>(),
                    0,
                    filters?.PageNumber ?? 1,
                    filters?.PageSize ?? 10
                );
            }
            var cacheKey = GenerateCacheKey(filters);

            if (_cache.TryGetValue(cacheKey, out PagedResponse<TrackSearchResultDto> cachedResult))
            {
                return cachedResult;
            }

            var result = await _repo.SearchTracksAsync(filters, cancellationToken);

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));

            _cache.Set(cacheKey, result, cacheOptions);

            return result;
        }
        private void SanitizeFilters(TrackFilters filters)
        {
            if (filters == null) return;

            filters.TrackName = filters.TrackName?.Trim();
            filters.Artist = filters.Artist?.Trim();
            filters.Album = filters.Album?.Trim();
            filters.Genre = filters.Genre?.Trim();
        }
        private bool HasValidFilters(TrackFilters filters)
        {
            if (filters == null) return false;

            return !string.IsNullOrWhiteSpace(filters.TrackName) ||
                   !string.IsNullOrWhiteSpace(filters.Artist) ||
                   !string.IsNullOrWhiteSpace(filters.Album) ||
                   !string.IsNullOrWhiteSpace(filters.Genre);
        }
        private string GenerateCacheKey(TrackFilters filters)
        {
            
            return filters == null
                ? "tracks_search_null"
                : $"tracks_search_{filters.PageNumber}_" +
                  $"{filters.PageSize}_" +
                  $"{(string.IsNullOrWhiteSpace(filters.Album) ? "null" : filters.Album.Trim().ToLowerInvariant())}_" +
                  $"{(string.IsNullOrWhiteSpace(filters.Genre) ? "null" : filters.Genre.Trim().ToLowerInvariant())}_" +
                  $"{(string.IsNullOrWhiteSpace(filters.Artist) ? "null" : filters.Artist.Trim().ToLowerInvariant())}_" +
                  $"{(string.IsNullOrWhiteSpace(filters.TrackName) ? "null" : filters.TrackName.Trim().ToLowerInvariant())}";
        }
    }
}
