using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Graphql;

public class Query
{
    public async Task<List<BaseAggregate>> Aggregates([Service] AppDbContext context)
    {
        return await context.BaseAggregate.ToListAsync();
    }
}