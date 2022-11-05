using System.Globalization;

namespace backend.Data;

public class Book : BaseAggregate
{
    public string? Status { get; set; }
    public string? Title { get; set; }
    public string? Authors { get; set; }

    public Book()
    {
        base.New();
    }
}