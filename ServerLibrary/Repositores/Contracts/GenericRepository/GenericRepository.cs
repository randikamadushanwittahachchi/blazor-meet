using BasedLibrary.DTOs.Response;

namespace ServerLibrary.Repositores.Contracts.GenericRepository;

/// <summary>
/// Defines a generic repository for general CRUD operation.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public interface GenericRepository<T> where T : class
{
    /// <summary>
    /// Retrieves all entities of type <typeparamref name="T"/>
    /// </summary>
    /// <returns>A task containing a <see cref="DataResponse{T}"/> with the list of entities</returns>
    Task<DataResponse<T>> GetAllAsync();

    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="model">The entity to create</param>
    /// <returns>A task containing a <see cref="GeneralResponse"/> indicating success or failuer.</returns>
    Task<GeneralResponse> CreateAsync(T model);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="model">The entity with updated data</param>
    /// <returns>A task containing a <see cref="GeneralResponse"/> indicating success or failuer</returns>
    Task<GeneralResponse> UpdateAsync(T model);

    /// <summary>
    /// Deletes an entity by its unique identifier
    /// </summary>
    /// <param name="id">the unique identifier of entity.</param>
    /// <returns>A task containing a <see cref="GeneralResponse"/> indicating success or failuer</returns>
    Task<GeneralResponse> DeleteByIdAsync(int id);
}
