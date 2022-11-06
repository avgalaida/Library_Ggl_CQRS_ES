namespace backend.Data;

public class CreateBookEvent : BaseEvent
{
    public string? Status { get; set; }
    public string? Title { get; set; }
    public string? Authors { get; set; }

    public Book ApplyOn(Book book)
    {
        book.Status = this.Status;
        book.Title = this.Title;
        book.Authors = this.Authors;
        
        return book;
    }
}

public class DeleteBookEvent : BaseEvent
{
    public Book ApplyOn(Book book)
    {
        book.Status = "Недоступна";

        return book;
    }
}

public class RestoreBookEvent : BaseEvent
{
    public string? Status { get; set; }
    public Book ApplyOn(Book book)
    {
        book.Status = this.Status;

        return book;
    }
}

public class ChangeBookTitleEvent : BaseEvent
{
    public string? Title { get; set; }
    public Book ApplyOn(Book book)
    {
        book.Title = this.Title;

        return book;
    }
}

public class ChangeBookAuthorsEvent : BaseEvent
{
    public string? Authors { get; set; }
    public Book ApplyOn(Book book)
    {
        book.Authors = this.Authors;

        return book;
    }
}

public class RollbackBookEvent : BaseEvent
{
    public string? Status { get; set; }
    public string? Title { get; set; }
    public string? Authors { get; set; }

    public Book ApplyOn(Book book)
    {
        book.Status = this.Status;
        book.Title = this.Title;
        book.Authors = this.Authors;
        
        return book;
    }
}