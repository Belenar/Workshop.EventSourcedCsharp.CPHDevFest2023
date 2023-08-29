using BeerSender.Domain.Infrastructure;

namespace BeerSender.Web.Event_store;

public class Event_service
{
    private readonly Event_context _db_context;

    public Event_service(Event_context db_context)
    {
        _db_context = db_context;
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

