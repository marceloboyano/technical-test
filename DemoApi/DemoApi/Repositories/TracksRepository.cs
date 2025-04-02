using Dapper;
using DemoApi.Models;
using DemoApi.Models.DTOs;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Caching.Memory;

namespace DemoApi.Repositories
{
    public class TracksRepository
    {
        private static readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        public async Task<Track?> GetById(int trackId)
        {
            var query = "SELECT * FROM tracks WHERE TrackId = @Trackid";
            using var connection = new SqliteConnection(@"Data Source=Assets\chinook.db");
            await connection.OpenAsync();
            var track = await connection.QueryAsync<Track>(query, new { Trackid = trackId });
            return track.SingleOrDefault();
        }

        public async Task<PagedResponse<TrackSearchResultDto>> SearchTracksAsync(TrackFilters filters)
        {
            var cacheKey = $"tracks_{filters.GetHashCode()}";
            if (_cache.TryGetValue(cacheKey, out PagedResponse<TrackSearchResultDto> cachedResult))
                return cachedResult;

            var query = @"
               SELECT
                t.TrackId,
                t.Name,
                t.Composer,
                t.Milliseconds AS DurationMs,
                t.UnitPrice AS Price,
                a.Title AS AlbumTitle,
                ar.Name AS ArtistName,
                g.Name AS GenreName,
                COUNT(*) OVER() AS TotalCount
               FROM tracks t
                LEFT JOIN albums a ON t.AlbumId = a.AlbumId
                LEFT JOIN artists ar ON a.ArtistId = ar.ArtistId
                LEFT JOIN genres g ON t.GenreId = g.GenreId
                WHERE 
                    (@TrackName IS NULL OR t.Name LIKE '%' || @TrackName || '%')
                    AND (@Artist IS NULL OR ar.Name LIKE '%' || @Artist || '%')
                    AND (@Album IS NULL OR a.Title LIKE '%' || @Album || '%')
                    AND (@Genre IS NULL OR g.Name LIKE '%' || @Genre || '%')
                ORDER BY t.Name
                LIMIT @PageSize OFFSET @Offset";

            using var connection = new SqliteConnection(@"Data Source=Assets\chinook.db");
            await connection.OpenAsync();

            var parameters = new
            {
                TrackName = string.IsNullOrEmpty(filters.TrackName) ? null : filters.TrackName,
                Artist = string.IsNullOrEmpty(filters.Artist) ? null : filters.Artist,
                Album = string.IsNullOrEmpty(filters.Album) ? null : filters.Album,
                Genre = string.IsNullOrEmpty(filters.Genre) ? null : filters.Genre,
                filters.PageSize,
                Offset = (filters.PageNumber - 1) * filters.PageSize
            };

            var results = await connection.QueryAsync<TrackSearchResultDto>(query, parameters);

            var totalCount = results.FirstOrDefault()?.TotalCount ?? 0;
            var pagedResult = new PagedResponse<TrackSearchResultDto>(
                results,
                totalCount,
                filters.PageNumber,
                filters.PageSize
            );

            _cache.Set(cacheKey, pagedResult, TimeSpan.FromMinutes(10));
            return pagedResult;
        }

    }
}
