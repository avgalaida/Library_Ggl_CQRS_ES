using backend.Domain;
using backend.Service;

namespace backend.Graphql;

public class BookMutation
{
    private readonly BookService _bookService;
    private readonly BookEventPublisher _bookEventPublisher;

    public BookMutation(BookService bookService, BookEventPublisher bookEventPublisher)
    {
        _bookService = bookService;
        _bookEventPublisher = bookEventPublisher;
    }
    
    public async Task<Book> CreateBook(CreateBookEvent request)
    {
        var book = await _bookService.CreateBook(request);
        await _bookEventPublisher.PublishCreatedBook(book);
        
        return book;
    }
    
    public async Task<Book> DeleteBook(string id)
    {
        var book = await _bookService.DeleteBook(id);
        await _bookEventPublisher.PublishDeletedBook(book);

        return book;
    }

    public async Task<Book> RestoreBook(RestoreBookEvent request )
    {
        var book = await _bookService.RestoreBook(request);
        await _bookEventPublisher.PublishRestoredBook(book);

        return book;
    }

    public async Task<Book> ChangeBookTitle(ChangeBookTitleEvent request )
    {
        var book = await _bookService.ChangeBookTitle(request);
        await _bookEventPublisher.PublishBookTitleChanged(book);
        
        return book;
    }

    public async Task<Book> ChangeBookAuthors(ChangeBookAuthorsEvent request )
    {
        var book = await _bookService.ChangeBookAuthors(request);
        await _bookEventPublisher.PublishBookAuthorsChanged(book);
        
        return book;
    }
    
    public async Task<Book> RollbackBook(RollbackBookEvent request )
    {
        var book = await _bookService.RollbackBook(request);
        await _bookEventPublisher.PublishRollbackedBook(book);
        
        return book;
    }
}