using Dapper;
using DemoApi.DTOs.Requests;
using DemoApi.DTOs.Responses;
using DemoApi.Models;
using DemoApi.Repositories.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Diagnostics;
using static DemoApi.Repositories.TracksRepository;

namespace DemoApi.Repositories
{
    public class TracksRepository : BaseRepository<Track>, ITracksRepository
    {
        private readonly IMemoryCache _cache;
        public TracksRepository(IDbConnection connection, IMemoryCache cache) : base(connection)
        {
            _cache = cache;
        }
        protected override string TableName => "tracks";
        protected override string AllColumns => "TrackId, Name, AlbumId, MediaTypeId, GenreId, Composer, Milliseconds, Bytes, UnitPrice";
        protected override string InsertColumns => "Name, AlbumId, MediaTypeId, GenreId, Composer, Milliseconds, Bytes, UnitPrice";
        protected override string InsertValues => "@Name, @AlbumId, @MediaTypeId, @GenreId, @Composer, @Milliseconds, @Bytes, @UnitPrice";
        protected override string UpdateSetClause => "Name = @Name, AlbumId = @AlbumId, MediaTypeId = @MediaTypeId, GenreId = @GenreId, " +
                                                  "Composer = @Composer, Milliseconds = @Milliseconds, Bytes = @Bytes, UnitPrice = @UnitPrice";
        protected override string DefaultSortField => "Name";
        protected override string IdColumn => "TrackId";

        public async Task<PagedResponse<TrackSearchResultDto>> SearchTracksAsync(TrackFilters filters, CancellationToken cancellationToken)
        {
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
            await connection.OpenAsync(cancellationToken);

            var parameters = new
            {
                TrackName = string.IsNullOrEmpty(filters.TrackName) ? null : filters.TrackName,
                Artist = string.IsNullOrEmpty(filters.Artist) ? null : filters.Artist,
                Album = string.IsNullOrEmpty(filters.Album) ? null : filters.Album,
                Genre = string.IsNullOrEmpty(filters.Genre) ? null : filters.Genre,
                filters.PageSize,
                Offset = (filters.PageNumber - 1) * filters.PageSize
            };

            var results = (await connection.QueryAsync<TrackSearchResultDto>(query, parameters)).AsList() ?? [];

            var totalCount = results.Count > 0 ? results[0].TotalCount : 0;
            return new PagedResponse<TrackSearchResultDto>(
                results,
                totalCount,
                filters.PageNumber,
                filters.PageSize
            );
        }
    }


}
