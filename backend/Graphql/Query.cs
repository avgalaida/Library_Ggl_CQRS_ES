using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Graphql;

public class Query
{
    public async Task<List<Book>> Books([Service] AppDbContext context)
    {
        List<BaseAggregate> aggregates = await Aggregates(context);
        List<BaseEvent> events = await Events(context);

        var aemap = new Dictionary<BaseAggregate, List<BaseEvent>>();

        BaseAggregate key = new BaseAggregate();
        foreach (var eEvent in events)
        {
            foreach (var aggregate in aggregates)
            {
                if (eEvent.AggregateId == aggregate.Id )
                {
                    key = aggregate;
                    break;
                }
            }
            if (aemap.ContainsKey(key))
            {
                aemap[key].Add(eEvent);
            }
            else
            {
                aemap.Add(key, new List<BaseEvent>{eEvent});
            }
        }
        
        Book book;
        List<Book> books = new List<Book>();
        foreach (var aePair in aemap)
        {
            book = new Book();
            book.From(aePair.Key);
            foreach (var eEvent in aePair.Value)
            {
                book = eEvent.ApplyTo(book);
            }
            books.Add(book);
        }
        return books;
    }
    
    private async Task<List<BaseAggregate>> Aggregates([Service] AppDbContext context)
    {
        return await context.BaseAggregate.ToListAsync();
    }

    private async Task<List<BaseEvent>> Events([Service] AppDbContext context)
    {
        return await context.BaseEvent.ToListAsync();
    }
}