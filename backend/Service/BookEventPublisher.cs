using backend.Domain;
using backend.Graphql;
using HotChocolate.Subscriptions;

namespace backend.Service;

public class BookEventPublisher
{

    private readonly ITopicEventSender _sender;

    public BookEventPublisher(ITopicEventSender sender)
    {
        _sender = sender;
    }

    public async Task<Book> PublishCreatedBook(Book book)
    {
        await _sender.SendAsync(nameof(BookSubscription.OnBookCreated), book);
        return book;
    }
    
    public async Task<Book> PublishDeletedBook(Book book)
    {
        await _sender.SendAsync(nameof(BookSubscription.OnBookDeleted), book);
        return book;
    }
    
    public async Task<Book> PublishRestoredBook(Book book)
    {
        await _sender.SendAsync(nameof(BookSubscription.OnBookRestored), book);
        return book;
    }
    
    public async Task<Book> PublishBookTitleChanged(Book book)
    {
        await _sender.SendAsync(nameof(BookSubscription.OnBookTitleChanged), book);
        return book;
    }
    
    public async Task<Book> PublishBookAuthorsChanged(Book book)
    {
        await _sender.SendAsync(nameof(BookSubscription.OnBookAuthorsChanged), book);
        return book;
    }
    
    public async Task<Book> PublishRollbackedBook(Book book)
    {
        await _sender.SendAsync(nameof(BookSubscription.OnBookRollbacked), book);
        return book;
    }
}