using backend.Data;

namespace backend.Graphql;

public class Subscription
{
    [Subscribe]
    public Book OnBookCreated([EventMessage] Book book) => book;
    
    [Subscribe]
    public DeleteBookEvent OnBookDeleted([EventMessage] DeleteBookEvent delta) => delta;
    
    [Subscribe]
    public RestoreBookEvent OnBookRestored([EventMessage] RestoreBookEvent delta) => delta;
    
    [Subscribe]
    public ChangeBookTitleEvent OnBookTitleChanged([EventMessage] ChangeBookTitleEvent delta) => delta;
    
    [Subscribe]
    public ChangeBookAuthorsEvent OnBookAuthorsChanged([EventMessage] ChangeBookAuthorsEvent delta) => delta;
    
}