using BasedLibrary.Entities.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ServerLibrary.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Authentication
    public DbSet<AppUser> AppUsers { get; set; }
}
