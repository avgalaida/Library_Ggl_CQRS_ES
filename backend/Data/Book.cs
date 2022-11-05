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

    public void From(BaseAggregate a)
    {
        this.Id = a.Id;
        this.LastRevision = a.LastRevision;
        this.CreatedAt = a.CreatedAt;
    }
}