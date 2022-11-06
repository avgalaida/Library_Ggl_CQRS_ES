using System.CodeDom.Compiler;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using Microsoft.CSharp;

namespace backend.Data;

public class BaseEvent
{
    public string? Id { get; set; }
    public string? AggregateId { get; set; }
    public string? CreatedAt { get; set; }
    public int? Revision { get; set; }
    public string? Data { get; set; }
    public string? EventType { get; set; }
    public  string? UserId { get; set; }
    
    public dynamic ApplyTo(dynamic aggregate)
    {
        dynamic eEvent = null;
        switch (EventType)
        {
            case "CreateBookEvent":
                eEvent = JsonSerializer.Deserialize<CreateBookEvent>(Data);
                break;
            case "DeleteBookEvent":
                eEvent = JsonSerializer.Deserialize<DeleteBookEvent>(Data);
                break; 
            case "RestoreBookEvent":
                eEvent = JsonSerializer.Deserialize<RestoreBookEvent>(Data);
                break;
            case "ChangeBookTitleEvent":
                eEvent = JsonSerializer.Deserialize<ChangeBookTitleEvent>(Data);
                break;
            case "ChangeBookAuthorsEvent":
                eEvent = JsonSerializer.Deserialize<ChangeBookAuthorsEvent>(Data);
                break;
        }
        return eEvent.ApplyOn(aggregate);
    }

    public void Register(BaseAggregate aggregate, dynamic eEvent)
    {
        this.Id = Guid.NewGuid().ToString();
        this.AggregateId = aggregate.Id;
        this.CreatedAt = DateTime.Now.ToString(CultureInfo.GetCultureInfo("ru-RU"));
        this.Revision = aggregate.LastRevision;
        this.Data = JsonSerializer.Serialize(eEvent);
        this.EventType = eEvent.GetType().Name;
        this.UserId = "TODO";
    }
}
