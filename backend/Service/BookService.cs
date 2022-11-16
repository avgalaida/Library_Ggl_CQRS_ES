using backend.Repository;
using backend.EsCqrs;
using backend.Domain;

namespace backend.Service;

public class BookService {
    private readonly IRepository _repository;

    public BookService(AppDbContext context)
    {
        _repository = new PostgresRepository(context);
    }

    public async Task<Book> CreateBook(CreateBookEvent eEvent)
    {
        var book = new Book();
        eEvent.Register(book, eEvent);
        book = eEvent.ApplyOn(book);
        
        await _repository.InsertAggregate(book);
        await _repository.UpgradeAggregateLastRevision(book.Id);
        await _repository.InsertEvent(eEvent);
        
        return book;
    }
    
    public async Task<Book> DeleteBook(string id )
    {
        var book = new Book(await _repository.GetAggregate(id));
        var eEvent = new DeleteBookEvent();
        eEvent.Register(book, eEvent);
        book = eEvent.ApplyOn(book);
        
        await _repository.UpgradeAggregateLastRevision(book.Id);
        await _repository.InsertEvent(eEvent);

        return book;
    }
    
    public async Task<Book> RestoreBook(RestoreBookEvent eEvent )
    {
        var book = new Book(await _repository.GetAggregate(eEvent.AggregateId));
        eEvent.Register(book, eEvent);
        book = eEvent.ApplyOn(book);

        await _repository.UpgradeAggregateLastRevision(book.Id);
        await _repository.InsertEvent(eEvent);

        return book;
    }
    
    public async Task<Book> ChangeBookTitle(ChangeBookTitleEvent eEvent )
    {
        var book = new Book(await _repository.GetAggregate(eEvent.AggregateId));
        eEvent.Register(book, eEvent);
        book = eEvent.ApplyOn(book);

        await _repository.UpgradeAggregateLastRevision(book.Id);
        await _repository.InsertEvent(eEvent);

        return book;
    }
    
    public async Task<Book> ChangeBookAuthors(ChangeBookAuthorsEvent eEvent )
    {
        var book = new Book(await _repository.GetAggregate(eEvent.AggregateId));
        eEvent.Register(book, eEvent);
        book = eEvent.ApplyOn(book);

        await _repository.UpgradeAggregateLastRevision(book.Id);
        await _repository.InsertEvent(eEvent);
        return book;
    }
    
    public async Task<Book> RollbackBook(RollbackBookEvent eEvent )
    {
        var book = new Book(await _repository.GetAggregate(eEvent.AggregateId));

        eEvent.Register(book, eEvent);
        book = eEvent.ApplyOn(book);

        await _repository.UpgradeAggregateLastRevision(book.Id);
        await _repository.InsertEvent(eEvent);

        return book;
    }
    
    public async Task<List<Book>> GetAllBooks()
    {
        var aggregates = await _repository.GetAllAggregates();
        var events = await _repository.GetAllEvents();

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
                var deserializedEvent = EventDeserializer.DeserializeEvent(eEvent, book);
                book = deserializedEvent.ApplyOn(book);
            }
            books.Add(book);
        }
        return books;
    }
    
    public async Task<Book> GetBookWithRevision(string id, int revisionLimit)
    {
        var bookBase = await _repository.GetAggregate(id);
        var book = new Book(bookBase);
        book.LastRevision = revisionLimit;

        var events = await _repository.GetCertainEvents(book.Id, revisionLimit);
        foreach (var eEvent in events)
        {
            var deserializedEvent = EventDeserializer.DeserializeEvent(eEvent, book);
            book = deserializedEvent.ApplyOn(book);
        }

        return book;
    }
    
}