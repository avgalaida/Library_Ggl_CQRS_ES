﻿using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Graphql;

public class Query
{
    public async Task<Book> GetBookWithRevision([Service] AppDbContext context, string id, int revision)
    {
        var bookBase = await context.BaseAggregate.FirstOrDefaultAsync(a => a.Id == id);
        var book = new Book(bookBase);
        book.LastRevision = 0;
        
        var events = context.BaseEvent.Where(e => e.AggregateId == book.Id && e.Revision < revision);
        foreach (var eEvent in events)
        {
            eEvent.ApplyTo(book);
            book.LastRevision++;
        }

        return book;
    }
    public async Task<List<Book>> GetBooks([Service] AppDbContext context)
    {
        var aggregates = await GetAggregates(context);
        var events = await GetEvents(context);

        var aemap = new Dictionary<BaseAggregate, List<BaseEvent>>();

        var key = new BaseAggregate();
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
        
        var books = new List<Book>();
        foreach (var aePair in aemap)
        {
            var book = new Book(aePair.Key);
            foreach (var eEvent in aePair.Value)
            {
                book = eEvent.ApplyTo(book);
            }
            books.Add(book);
        }
        return books;
    }
    
    private static async Task<List<BaseAggregate>> GetAggregates([Service] AppDbContext context)
    {
        return await context.BaseAggregate.ToListAsync();
    }

    private static async Task<List<BaseEvent>> GetEvents([Service] AppDbContext context)
    {
        return await context.BaseEvent.ToListAsync();
    }
}