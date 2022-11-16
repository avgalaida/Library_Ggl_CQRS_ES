using backend.Domain;
using backend.Repository;
using backend.Service;
using Microsoft.EntityFrameworkCore;

namespace backend.Graphql;

public class BookQuery
{
    private readonly BookService _bookService;

    public BookQuery(BookService bookService)
    {
        _bookService = bookService;
    }
    
    public async Task<Book> GetBookWithRevision(string id, int revision)
    {
        return await _bookService.GetBookWithRevision(id,revision);
    }
    public async Task<List<Book>> GetBooks()
    {
        return await _bookService.GetAllBooks();
    }
}