using Microsoft.EntityFrameworkCore;

namespace AspForDocker.AspDbContext;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}
