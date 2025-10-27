//using BasedLibrary.DTOs.Response;
//using BasedLibrary.Entities.Authentication;
//using ServerLibrary.Data;
//using ServerLibrary.Repositores.Contracts.Authentication;

//namespace ServerLibrary.Repositores.Implementations.Authentication;

//public class UserRoleRepository : IUserRoleRepository
//{
//    private readonly AppDbContext _context;
//    public UserRoleRepository(AppDbContext context)
//    {
//        _context = context;
//    }
//    public Task<GeneralResponse> CreateAsync(UserRole model)
//    {
//        throw new NotImplementedException();
//    }

//    public async Task<DataResponse<UserRole>> FindByUserIdAsync(int id)
//    {
//        var userRole = await _context.UserRoles.FirstOrDefault(ur => ur.UserId == id);
        
//    }
//}
