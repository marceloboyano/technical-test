using Dapper;
using DemoApi.Repositories.Interfaces;
using System.Data;

namespace DemoApi.Repositories
{
    public abstract class BaseRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IDbConnection _connection;
        protected abstract string TableName { get; }
        protected abstract string AllColumns { get; }
        protected abstract string InsertColumns { get; }
        protected abstract string InsertValues { get; }
        protected abstract string UpdateSetClause { get; }
        protected abstract string DefaultSortField { get; }
        protected virtual string IdColumn => $"{typeof(T).Name}Id";




        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<T>(
                 new CommandDefinition(
                     $"SELECT {AllColumns} FROM {TableName} ORDER BY {DefaultSortField} ASC",
                     cancellationToken: cancellationToken));
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            string sql = $"SELECT {AllColumns} FROM {TableName}  WHERE {IdColumn} = @id";
            return await _connection.QueryFirstOrDefaultAsync<T>(
           new CommandDefinition(
               sql,
               new { id },
               cancellationToken: cancellationToken));
        }

        public async Task<int> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            var sql = $"INSERT INTO {TableName} ({InsertColumns}) VALUES ({InsertValues})";
            return await _connection.ExecuteScalarAsync<int>(
               new CommandDefinition(
                   sql + "; SELECT LAST_INSERT_ID();",
                   entity,
                   cancellationToken: cancellationToken));
        }

        public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            var sql = $"UPDATE {TableName} SET {UpdateSetClause} WHERE {IdColumn} = @{IdColumn}";
            int affected = await _connection.ExecuteAsync(
               new CommandDefinition(
                   sql,
                   entity,
                   cancellationToken: cancellationToken));
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            string sql = $"DELETE FROM {TableName} WHERE {IdColumn} = @id";
            int affected = await _connection.ExecuteAsync(
                 new CommandDefinition(
                     sql,
                     new { id },
                     cancellationToken: cancellationToken));
            return affected > 0;
        }
    }
}