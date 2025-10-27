using BasedLibrary.DTOs.Response;
using BasedLibrary.Entities.Authentication;

namespace ServerLibrary.Repositores.Contracts.Authentication;

/// <summary>
/// Defines the contract for system role repository operations,
/// including retrieval and creation of system-role with the authentication system.
/// </summary>
public interface ISystemRoleRepository
{
    /// <summary>
    /// Retrieves a system role by its name
    /// </summary>
    /// <param name="name"> the name of the role to retrieve</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// the task result contains a <see cref="DataResponse{T}"/> of the type <see cref="SystemRole"/>.
    /// </returns>
    Task<DataResponse<SystemRole>> GetByNameAsync(string name);

    /// <summary>
    /// Retrieves a system role by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the system role</param>
    /// <returns>
    /// A task represents the asynchronous operation.
    /// the task result contains a <see cref="DataResponse{T}"/> of type <see cref="SystemRole"/>.
    /// </returns>
    Task<DataResponse<SystemRole>> GetByIdAsync(int id);

    /// <summary>
    /// Create a new system role with the system role name.
    /// </summary>
    /// <param name="model">The model of the new system role</param>
    /// <returns>
    /// a task represents the asynchronous operation.
    /// the task result contains a <see cref="GeneralResponse"/> indicationg success or failure.
    /// </returns>
    Task<GeneralResponse> CreateAsync(SystemRole model);
}
