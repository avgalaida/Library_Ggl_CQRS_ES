using System.Globalization;
using System.Text.Json;
using backend.Domain;

namespace backend.EsCqrs;

public class BaseEvent
{
    public string? Id { get; set; }
    public string? AggregateId { get; set; }
    public string? CreatedAt { get; set; }
    public int? Revision { get; set; }
    public string? Data { get; set; }
    public string? EventType { get; set; }
    public  string? UserId { get; set; }
    
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
