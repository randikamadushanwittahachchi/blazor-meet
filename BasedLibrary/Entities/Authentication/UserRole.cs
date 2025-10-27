using BasedLibrary.Constants;
using System.ComponentModel.DataAnnotations;

namespace BasedLibrary.Entities.Authentication;

public class UserRole
{
    public int Id { get; set; }

    [Required(ErrorMessage =ValidationMessages.AppUserIdRequired)]
    [Range(1, int.MaxValue, ErrorMessage =ValidationMessages.InvalidAppUserId)]
    public int UserId { get; set; }

    [Required(ErrorMessage =ValidationMessages.SystemRoleRequired)]
    [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.InvalidSystemRoleId)]
    public int RoleId { get; set; }
}
