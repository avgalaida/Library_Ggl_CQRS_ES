using backend.EsCqrs;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class PostgresRepository : IRepository
{
    private readonly AppDbContext _context;

    public PostgresRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BaseAggregate>> GetAllAggregates()
    {
        return await _context.BaseAggregate.ToListAsync();
    }

    public async Task<List<BaseEvent>> GetCertainEvents(string aggregateId, int revisionLimit)
    {
        return await _context.BaseEvent.Where(e => 
            e.AggregateId == aggregateId && e.Revision < revisionLimit).ToListAsync();
    }
    
    public async Task<List<BaseEvent>> GetAllEvents()
    {
        return await _context.BaseEvent.ToListAsync();
    }
    
    public async Task<BaseAggregate> InsertAggregate(BaseAggregate aggregate)
    {
        _context.BaseAggregate.Add(aggregate);
        await _context.SaveChangesAsync();
        return aggregate;
    }
    
    public async Task<BaseAggregate> GetAggregate(string id)
    {
        var aggregate = await _context.BaseAggregate.FirstOrDefaultAsync(a => a.Id == id);
        return aggregate;
    }
    
    public async Task<BaseAggregate> UpgradeAggregateLastRevision(string id)
    {
        var aggregate = await _context.BaseAggregate.FirstOrDefaultAsync(a => a.Id == id);
        aggregate.LastRevision++;
        await _context.SaveChangesAsync();
        return aggregate;
    }
    
    public async Task<BaseEvent> InsertEvent(BaseEvent eEvent)
    {
        _context.BaseEvent.Add(eEvent);
        await _context.SaveChangesAsync();
        return eEvent;
    }
}