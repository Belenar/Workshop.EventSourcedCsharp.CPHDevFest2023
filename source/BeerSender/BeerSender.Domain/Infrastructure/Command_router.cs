using BeerSender.Domain.Boxes;
using BeerSender.Domain.Boxes.Command_handlers;

namespace BeerSender.Domain.Infrastructure;

public class Command_router
{
    private readonly Func<Guid, IEnumerable<object>> _event_stream;
    private readonly Action<Guid, object> _save_event;

    public Command_router(
        Func<Guid, IEnumerable<object>> event_stream,
        Action<Guid, object> save_event
        )
    {
        _event_stream = event_stream;
        _save_event = save_event;
    }

    public void Handle_command(Command command)
    {
        var aggregate_id = command.Aggregate_id;

        var events = _event_stream(aggregate_id);
        var new_events = new List<object>();

        switch(command)
        {
            case Close_box close_box:
                var aggregate = new Box_aggregate(new Box());
                foreach (var @event in events)
                {
                    aggregate.Apply((dynamic)@event);
                }
                new_events.AddRange(new Close_box_handler(aggregate.Root_entity).Handle(close_box));
                break;
        }

        foreach (object new_event in new_events)
        {
            _save_event(aggregate_id, new_event);
        }
    }
}

