using CQRS.Core.Events;

namespace CQRS.Core.Domain;

public interface IEventStoreRepository
{
    Task SaveAsync(EventModel @event);
    // Task<List<EventModel>> GetAllAsync();
    Task<List<EventModel>> FindByAggregateId(Guid aggregateId);
    // Task<List<EventModel>> GetEventsAsync(Guid aggregateIdentifier, int fromVersion);
    // Task<List<EventModel>> GetEventsAsync(Guid aggregateIdentifier, int fromVersion, int toVersion);
}