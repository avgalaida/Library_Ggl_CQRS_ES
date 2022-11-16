using backend.Domain;

namespace backend.Graphql;

public class BookSubscription
{
    [Subscribe]
    public Book OnBookCreated([EventMessage] Book book) => book;

    [Subscribe]
    public Book OnBookDeleted([EventMessage] Book delta) => delta;

    [Subscribe]
    public Book OnBookRestored([EventMessage] Book delta) => delta;

    [Subscribe]
    public Book OnBookTitleChanged([EventMessage] Book delta) => delta;

    [Subscribe]
    public Book OnBookAuthorsChanged([EventMessage] Book delta) => delta;

    [Subscribe]
    public Book OnBookRollbacked([EventMessage] Book delta) => delta;

}