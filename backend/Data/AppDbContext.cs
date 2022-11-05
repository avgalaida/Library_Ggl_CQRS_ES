using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class AppDbContext : DbContext
{
    public DbSet<BaseAggregate> BaseAggregate => Set<BaseAggregate>();
    public DbSet<BaseEvent> BaseEvent => Set<BaseEvent>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}