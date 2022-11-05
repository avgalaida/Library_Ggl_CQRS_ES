using backend.Data;
using HotChocolate.Subscriptions;

namespace backend.Graphql;

public class Mutation
{
    public async Task<Book> CreateBook([Service] AppDbContext context,
        [Service] ITopicEventSender sender, CreateBookEvent bookEvent)
    {
        Book book = new Book();
        bookEvent.Register(book, bookEvent);
        book = bookEvent.ApplyOn(book);

        await InsertAggregate(context, book);
        await InsertEvent(context, bookEvent);
        await sender.SendAsync(nameof(Subscription.OnBookCreated), book);

        return book;
    }
    private async Task<BaseAggregate> InsertAggregate([Service] AppDbContext context, BaseAggregate aggregate)
    {
        context.BaseAggregate.Add(aggregate);
        await context.SaveChangesAsync();
        return aggregate;
    }

    private async Task<BaseAggregate> UpgradeAggregate([Service] AppDbContext context, BaseAggregate aggregate)
    {
        context.BaseAggregate.Update(aggregate);
        await context.SaveChangesAsync();
        return aggregate;
    }
    private async Task<BaseEvent> InsertEvent([Service] AppDbContext context, BaseEvent eEvent)
    {
        context.BaseEvent.Add(eEvent);
        await context.SaveChangesAsync();
        return eEvent;
    }
}