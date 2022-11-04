using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Graphql;

public class Query
{
    public async Task<List<Book>> AllBookAsync([Service] AppDbContext context)
    {
        return await context.Book.ToListAsync();
    }
}