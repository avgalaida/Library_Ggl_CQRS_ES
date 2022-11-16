using backend.EsCqrs;

namespace backend.Repository;

public interface IRepository
{
    Task<List<BaseAggregate>> GetAllAggregates();
    Task<BaseAggregate> InsertAggregate(BaseAggregate aggregate);
    Task<BaseAggregate> GetAggregate(string id);
    Task<BaseAggregate> UpgradeAggregateLastRevision(string id);
    Task<BaseEvent> InsertEvent(BaseEvent eEvent);
    Task<List<BaseEvent>> GetAllEvents();
    Task<List<BaseEvent>> GetCertainEvents(string aggregateId, int revisionLimit);
}