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