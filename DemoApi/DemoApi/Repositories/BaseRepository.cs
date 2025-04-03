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
        protected virtual string IdColumn => $"{typeof(T).Name}Id";



        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Retrieves all entities from the database with optional sorting
        /// </summary>
        /// <param name="orderBy"></param>
        /// <returns>A collection of all entities, optionally sorted by the specified columns</returns>
        public async Task<IEnumerable<T>> GetAllAsync(string? orderBy)
        {
            var orderClause = string.IsNullOrWhiteSpace(orderBy)
                ? string.Empty
                : $" ORDER BY {orderBy}";

            return await _connection.QueryAsync<T>(
                $"SELECT {AllColumns} FROM {TableName}{orderClause}");
        }
        /// <summary>
        /// Retrieves a single entity by its unique identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The requested entity if found; otherwise, <c>null</c> if no entity matches the specified ID</returns>
        public async Task<T?> GetByIdAsync(int id)
        {
            string sql = $"SELECT {AllColumns} FROM {TableName}  WHERE {IdColumn} = @id";
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, new { id });
        }
        /// <summary>
        /// Creates a new entity in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The database-generated ID of the newly created entity</returns>
        public async Task<int> CreateAsync(T entity)
        {
            var sql = $"INSERT INTO {TableName} ({InsertColumns}) VALUES ({InsertValues})";
            return await _connection.ExecuteScalarAsync<int>(sql + "; SELECT LAST_INSERT_ID();", entity);
        }
        /// <summary>
        /// Updates an existing entity in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns><c>true</c> if the entity was found and updated successfully;
        /// <c>false</c> if no entity with the specified ID exists
        /// </returns>
        public async Task<bool> UpdateAsync(T entity)
        {
            var sql = $"UPDATE {TableName} SET {UpdateSetClause} WHERE {IdColumn} = @{IdColumn}";
            int affected = await _connection.ExecuteAsync(sql, entity);
            return affected > 0;
        }
        /// <summary>
        /// Deletes an entity from the database by its primary key.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// <c>true</c> if one or more rows were affected (entity was found and deleted);
        /// <c>false</c> if no rows were affected (entity with specified ID doesn't exist).
        /// </returns>
        public async Task<bool> DeleteAsync(int id)
        {
            string sql = $"DELETE FROM {TableName} WHERE {IdColumn} = @id";
            int affected = await _connection.ExecuteAsync(sql, new { id });
            return affected > 0;
        }
    }
}