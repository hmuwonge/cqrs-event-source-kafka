using CQRS.Core.Domain;
using CQRS.Core.Events;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Post.Cmd.Infrastructure.Config;

namespace Post.Cmd.Infrastructure.Repositories;

public class EventStoreRepository:IEventStoreRepository
{
    private readonly IMongoCollection<EventModel> _eventStoreCollection;
    
    public EventStoreRepository(IOptions<MongoDbContext> mongoDbContext)
    {
        var mongoClient = new MongoClient(mongoDbContext.Value.ConnectionString);
        var database = mongoClient.GetDatabase(mongoDbContext.Value.DatabaseName);
        _eventStoreCollection = database.GetCollection<EventModel>(mongoDbContext.Value.EventStoreCollectionName);
    }
    
    public async Task SaveAsync(EventModel @event)
    {
        await  _eventStoreCollection.InsertOneAsync(@event).ConfigureAwait(false);
    }

    public async Task<List<EventModel>> FindByAggregateId(Guid aggregateId)
    {
        return await _eventStoreCollection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync().ConfigureAwait(false);

    }
}