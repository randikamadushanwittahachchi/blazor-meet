using BasedLibrary.DTOs.Response;
using BasedLibrary.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositores.Contracts.Authentication;
/// <summary>
/// Defined contact for managing user-role relasionships with in authentication systems.
/// </summary>
public interface IUserRoleRepository
{
    /// <summary>
    /// Retrieves a user-role entity by the specify user id.
    /// </summary>
    /// <param name="id">the unique indetifier of the user.</param>
    /// <returns>
    /// A <see cref="DataResponse{T}"/> containing the <see cref="UserRole"/>
    /// associated with given user id, or an appropriate error message if not found.
    /// </returns>
    Task<DataResponse<UserRole>> FindByUserIdAsync(int id);

    /// <summary>
    /// Create new user-role association.
    /// </summary>
    /// <param name="model"> The <see cref="UserRole"/> entity to create.</param>
    /// <returns>
    /// A <see cref="GeneralResponse"/> indicating the success or failuer of the operation.
    /// </returns>
    Task<GeneralResponse> CreateAsync(UserRole model);
}
