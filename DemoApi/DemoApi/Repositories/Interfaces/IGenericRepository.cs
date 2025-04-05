namespace DemoApi.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities from the database with optional sorting
        /// </summary>        
        /// <param name="cancellationToken"></param>
        /// <returns>A collection of all entities, optionally sorted by the specified columns</returns>        
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a single entity by its unique identifier
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The requested entity if found; otherwise, <c>null</c> if no entity matches the specified ID</return> 
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new entity in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The database-generated ID of the newly created entity</returns>       
        Task<int> CreateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing entity in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> if the entity was found and updated successfully;
        /// <c>false</c> if no entity with the specified ID exists
        /// </returns>
        Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an entity from the database by its primary key.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <c>true</c> if one or more rows were affected (entity was found and deleted);
        /// <c>false</c> if no rows were affected (entity with specified ID doesn't exist).
        /// </returns>
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
