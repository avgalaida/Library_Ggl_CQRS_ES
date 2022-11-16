using System.Text.Json;
using backend.Domain;
using backend.EsCqrs;

namespace backend.Service;

public static class EventDeserializer
{
    public static dynamic DeserializeEvent(BaseEvent baseEvent,dynamic aggregate)
    {
        var particularEvent = Activator.CreateInstance(Type.GetType(baseEvent.EventType));
        particularEvent = JsonSerializer.Deserialize(baseEvent.Data, Type.GetType(baseEvent.EventType));
        return particularEvent;
    }
}
