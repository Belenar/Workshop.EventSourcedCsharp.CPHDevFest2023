using BeerSender.Domain.Infrastructure;
using BeerSender.Web.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BeerSender.Web.Event_store;

public class Event_service
{
    private readonly Event_context _db_context;
    private readonly IHubContext<EventHub> _hub_context;

    public Event_service(Event_context db_context,
        IHubContext<EventHub> hub_context)
    {
        _db_context = db_context;
        _hub_context = hub_context;
    }

    public IEnumerable<Event_message> Get_events(Guid aggregate_id)
    {
        var events = _db_context.Events
            .Where(e => e.Aggregate_id == aggregate_id)
            .OrderBy(e => e.Event_Number)
            .Select(a => new Event_message(
                a.Aggregate_id,
                a.Event_Number,
                a.Event));

        return events;
    }

    public void Save_event(Event_message message)
    {
        Save_to_database(message);
        Publish_to_hub(message);
    }

    private void Publish_to_hub(Event_message message)
    {
        _hub_context.Clients.Group(message.Aggregate_id.ToString())
            .SendAsync("publish_event", message.Aggregate_id, message.Event);
    }

    private void Save_to_database(Event_message message)
    {
        _db_context.Events.Add(
            new Aggregate_event
            {
                Aggregate_id = message.Aggregate_id,
                Event_Number = message.Event_number,
                Event = message.Event
            });
    }

    public void Commit()
    {
        _db_context.SaveChanges();
    }
}

