using backend.Data;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace backend.Graphql;

public class Mutation
{
    public async Task<Book> RollbackBook([Service] AppDbContext context, 
        [Service] ITopicEventSender sender, RollbackBookEvent eEvent )
    {
        var book = new Book(await GetAggregate(context, eEvent.AggregateId));
        
        eEvent.Register(book, eEvent);

        await UpgradeAggregateLastRevision(context, book.Id);
        await InsertEvent(context, eEvent);
            
        await sender.SendAsync(nameof(Subscription.OnBookRollbacked), eEvent);

        return book;
    }
    private static async Task<BaseAggregate> InsertAggregate([Service] AppDbContext context, BaseAggregate aggregate)
    {
        context.BaseAggregate.Add(aggregate);
        await context.SaveChangesAsync();   
        return aggregate;
    }
    private static async Task<BaseAggregate> GetAggregate([Service] AppDbContext context, string id)
    {
        var aggregate = await context.BaseAggregate.FirstOrDefaultAsync(a => a.Id == id);
        return aggregate;
    }
    private  static async Task<BaseAggregate> UpgradeAggregateLastRevision([Service] AppDbContext context, string id)
    {
        var aggregate = await context.BaseAggregate.FirstOrDefaultAsync(a => a.Id == id);
        aggregate.LastRevision++;
        await context.SaveChangesAsync();
        return aggregate;
    }
    private static async Task<BaseEvent> InsertEvent([Service] AppDbContext context, BaseEvent eEvent)
    {
        context.BaseEvent.Add(eEvent);
        await context.SaveChangesAsync();
        return eEvent;
    }
    public async Task<Book> CreateBook([Service] AppDbContext context,
        [Service] ITopicEventSender sender, CreateBookEvent eEvent)
    {
        var book = new Book();
        
        eEvent.Register(book, eEvent);
        
        book = eEvent.ApplyOn(book);

        await InsertAggregate(context, book);
        await UpgradeAggregateLastRevision(context, book.Id);
        await InsertEvent(context, eEvent);
        
        await sender.SendAsync(nameof(Subscription.OnBookCreated), book);

        return book;
    }
    public async Task<Book> DeleteBook([Service] AppDbContext context, 
    [Service] ITopicEventSender sender, string id )
    {
        var book = new Book(await GetAggregate(context, id));
        
        var eEvent = new DeleteBookEvent();
        eEvent.Register(book, eEvent);

        await UpgradeAggregateLastRevision(context, book.Id);
        await InsertEvent(context, eEvent);
            
        await sender.SendAsync(nameof(Subscription.OnBookDeleted), eEvent);

        return book;
    }
    
    public async Task<Book> RestoreBook([Service] AppDbContext context, 
        [Service] ITopicEventSender sender, RestoreBookEvent eEvent )
    {
        var book = new Book(await GetAggregate(context, eEvent.AggregateId));
        
        eEvent.Register(book, eEvent);

        await UpgradeAggregateLastRevision(context, book.Id);
        await InsertEvent(context, eEvent);
            
        await sender.SendAsync(nameof(Subscription.OnBookRestored), eEvent);

        return book;
    }
    
    public async Task<Book> ChangeBookTitle([Service] AppDbContext context, 
        [Service] ITopicEventSender sender, ChangeBookTitleEvent eEvent )
    {
        var book = new Book(await GetAggregate(context, eEvent.AggregateId));
        
        eEvent.Register(book, eEvent);

        await UpgradeAggregateLastRevision(context, book.Id);
        await InsertEvent(context, eEvent);
            
        await sender.SendAsync(nameof(Subscription.OnBookTitleChanged), eEvent);

        return book;
    }
    
    public async Task<Book> ChangeBookAuthors([Service] AppDbContext context, 
        [Service] ITopicEventSender sender, ChangeBookAuthorsEvent eEvent )
    {
        var book = new Book(await GetAggregate(context, eEvent.AggregateId));
        
        eEvent.Register(book, eEvent);

        await UpgradeAggregateLastRevision(context, book.Id);
        await InsertEvent(context, eEvent);
            
        await sender.SendAsync(nameof(Subscription.OnBookAuthorsChanged), eEvent);

        return book;
    }
}