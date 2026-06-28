using System.Data;
using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.Infrastructure.Stores;

public class EventStore:IEventStore
{
    private readonly IEventStoreRepository _eventRepository;
    public EventStore(IEventStoreRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    public async Task SaveEventAsync(Guid aggregateId, IEnumerable<EventModel> events, int expectedVersion)
    {
        var eventStream = await _eventRepository.FindByAggregateId(aggregateId);
        if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
        
            throw new ConcurrencyException();

        var version = expectedVersion;

        foreach (var @event in events)
        {
            version++;
            @event.Version = version;
            var eventType = @eventStream.GetType().Name;
            var eventModel = new EventModel
            {
                TimeStamp = DateTime.UtcNow,
                AggregateIdentifier = aggregateId,
                AggregateType = nameof(PostAggregate),
                Version = version,
                EventType = eventType,
                EventData = @event
            };
            
            await _eventRepository.SaveAsync(eventModel);
        }

    }

    public async Task<List<EventModel>> GetEventAsync(Guid aggregatedId)
    {
        var eventStream = await _eventRepository.FindByAggregateId(aggregatedId);

        if (eventStream == null || !eventStream.Any())
        {
            throw new AggregateNotFoundException("Incorrect post ID provided!");
        }

        return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
    }
    
    
}