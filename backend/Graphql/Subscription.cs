using backend.Data;

namespace backend.Graphql;

public class Subscription
{
    [Subscribe]
    public Book OnBookCreated([EventMessage] Book book) => book;
}