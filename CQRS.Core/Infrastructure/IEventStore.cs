using CQRS.Core.Events;

namespace CQRS.Core.Infrastructure;

public interface IEventStore
{
    Task SaveEventAsync(Guid aggregateId, IEnumerable<EventModel> events, int expectedVersion);
    Task<List<EventModel>> GetEventAsync(Guid aggregatedId);
}