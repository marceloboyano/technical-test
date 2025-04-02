using Dapper;
using DemoApi.Models;
using Microsoft.Data.Sqlite;

namespace DemoApi.Repositories
{
    public class GenresRepository
    {
        public async Task<IEnumerable<Genre>> GetAsync()
        {
            var query = "SELECT GenreId, Name FROM genres ORDER BY Name ASC;";
            using var connection = new SqliteConnection(@"Data Source=Assets\chinook.db");
            await connection.OpenAsync();
            var genres = (await connection.QueryAsync<Genre>(query)).AsList();
            return genres;
        }

        public async Task<IEnumerable<GenreSummary>> GetSongCountsPerGenreAsync()
        {
            var query = @"
            SELECT g.GenreId, g.Name, COUNT(t.TrackId) AS SongCount
            FROM genres g
            LEFT JOIN tracks t ON g.GenreId = t.GenreId
            GROUP BY g.GenreId, g.Name";
            using var connection = new SqliteConnection(@"Data Source=Assets\chinook.db");
            return await connection.QueryAsync<GenreSummary>(query);
        }
    }
}
