using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class AppDbContext : DbContext
{
    public DbSet<Book> Book => Set<Book>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}