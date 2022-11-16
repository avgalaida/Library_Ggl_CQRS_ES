using System.Text.Json;
using backend.Domain;
using backend.EsCqrs;

namespace backend.Service;

public static class EventDeserializer
{
    public static dynamic DeserializeEvent(BaseEvent baseEvent,dynamic aggregate)
    {
        dynamic particularEvent = null;
        switch (baseEvent.EventType)
        {
            case "CreateBookEvent":
                particularEvent = JsonSerializer.Deserialize<CreateBookEvent>(baseEvent.Data);
                break;
            case "DeleteBookEvent":
                particularEvent = JsonSerializer.Deserialize<DeleteBookEvent>(baseEvent.Data);
                break; 
            case "RestoreBookEvent":
                particularEvent = JsonSerializer.Deserialize<RestoreBookEvent>(baseEvent.Data);
                break;
            case "ChangeBookTitleEvent":
                particularEvent = JsonSerializer.Deserialize<ChangeBookTitleEvent>(baseEvent.Data);
                break;
            case "ChangeBookAuthorsEvent":
                particularEvent = JsonSerializer.Deserialize<ChangeBookAuthorsEvent>(baseEvent.Data);
                break;
            case "RollbackBookEvent":
                particularEvent = JsonSerializer.Deserialize<RollbackBookEvent>(baseEvent.Data);
                break;
        }
        return particularEvent;
    }
}