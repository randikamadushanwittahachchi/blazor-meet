using BasedLibrary.DTOs.Response;
using BasedLibrary.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Constants;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Contracts.Authentication;

namespace ServerLibrary.Repositores.Implementations.Authentication;

/// <summary>
/// provides data access and management functionality for system role.
/// Implements <see cref="ISystemRoleRepository"/>
/// </summary>
public class SystemRoleRepository : ISystemRoleRepository
{
    private readonly AppDbContext _context;
    public SystemRoleRepository(AppDbContext context)
    {
        _context = context;
    }

    ///<inheritdoc/>
    public async Task<DataResponse<SystemRole>> GetByIdAsync(int id)
    {
        var systemRole = await _context.SystemRoles.FindAsync(id);
        return systemRole is null 
            ? DataResponse<SystemRole>.Failure(ResponseMessages.Failuer)
            : DataResponse<SystemRole>.Success(systemRole, ResponseMessages.Success);
    }

    ///<inheritdoc/>
    public async Task<DataResponse<SystemRole>> GetByNameAsync(string name)
    {
        var systemRole = await _context.SystemRoles.FirstOrDefaultAsync(sr => string.Equals(sr.Name.Trim(), name.Trim(), StringComparison.OrdinalIgnoreCase ));
        return systemRole is null
            ? DataResponse<SystemRole>.Failure(ResponseMessages.Failuer)
            : DataResponse<SystemRole>.Success( systemRole, ResponseMessages.Success);
    }

    ///<inheritdoc/>
    public async Task<GeneralResponse> CreateAsync(SystemRole model)
    {
        bool isValid =  ValidationHelper.ValidateModel<SystemRole>(model);
        if (!isValid) return GeneralResponse.Failure(ResponseMessages.ValidationFailed);

        var existingRoleResponse = await GetByNameAsync(model.Name);
        if (existingRoleResponse.flag) return GeneralResponse.Failure(ResponseMessages.AlreadyExists);

        _context.SystemRoles.Add(model);
        await _context.SaveChangesAsync();
        return GeneralResponse.Success(ResponseMessages.CreatedSuccessfully);
    }

}
