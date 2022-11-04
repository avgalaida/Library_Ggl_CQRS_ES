using backend.Data;
using HotChocolate.Subscriptions;

namespace backend.Graphql;

public class Mutation
{
    public async Task<Book> SaveBookAsync([Service] AppDbContext context, 
        [Service] ITopicEventSender sender, Book book)
    {
        context.Book.Add(book);
        await context.SaveChangesAsync();
        await sender.SendAsync(nameof(Subscription.OnBookCreated), book);
        return book;
    }
    
    public async Task<Book> UpdateBookAsync([Service] AppDbContext context, Book updateBook)
    {
        context.Book.Update(updateBook);
        await context.SaveChangesAsync();
        return updateBook;
    }
}