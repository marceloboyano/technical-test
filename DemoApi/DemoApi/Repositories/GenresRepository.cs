using Dapper;
using DemoApi.DTOs.Responses;
using DemoApi.Models;
using DemoApi.Repositories.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;

namespace DemoApi.Repositories
{
    public class GenresRepository : BaseRepository<Genre>, IGenresRepository
    {

        public GenresRepository(IDbConnection connection) : base(connection) { }
        protected override string TableName => "genres";
        protected override string AllColumns => "GenreId, Name";
        protected override string InsertColumns => "GenreId,Name";
        protected override string InsertValues => "@GenreId, @Name";
        protected override string UpdateSetClause => "GenreId = @GenreId, Name = @Name";
        protected override string IdColumn => "GenreId";

        /// <summary>
        /// Retrieves a list of genres with their corresponding song counts, ordered by most songs to least.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GenreSummaryResponseDto>> GetSongCountsPerGenreAsync()
        {
            var query = @"
            SELECT 
                g.GenreId, 
                g.Name, 
                COUNT(t.TrackId) AS SongCount
            FROM genres g
            LEFT JOIN tracks t ON g.GenreId = t.GenreId
            GROUP BY g.GenreId, g.Name
            ORDER BY SongCount DESC";

            return await _connection.QueryAsync<GenreSummaryResponseDto>(query);
        }
    }
}
