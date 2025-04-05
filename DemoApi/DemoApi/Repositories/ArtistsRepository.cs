using Dapper;
using DemoApi.Models;
using DemoApi.Repositories.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;

namespace DemoApi.Repositories
{
    public class ArtistsRepository: BaseRepository<Artist>, IArtistsRepository
    {
        public ArtistsRepository(IDbConnection connection) : base(connection) { }
        protected override string TableName => "artists";
        protected override string AllColumns => "ArtistId, Name";
        protected override string InsertColumns => "ArtistId,Name";
        protected override string InsertValues => "@ArtistId, @Name";
        protected override string UpdateSetClause => "ArtistId = @ArtistId, Name = @Name";
        protected override string DefaultSortField => "Name";
        protected override string IdColumn => "ArtistId";
       
    }
}
