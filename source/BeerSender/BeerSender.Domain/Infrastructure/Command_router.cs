using System.Reflection;

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

        var new_events = Run_handler(events, command);

        foreach (object new_event in new_events)
        {
            _save_event(aggregate_id, new_event);
        }
    }

    private IEnumerable<object> Run_handler(IEnumerable<object> events, Command command)
    {
        var command_type = command.GetType();
        var handler_type = Command_handlers[command_type].Handler_type;
        var apply_method = Command_handlers[command_type].Apply_method;
        var handle_method = Command_handlers[command_type].Handle_method;

        var instance = Activator.CreateInstance(handler_type);

        apply_method.Invoke(instance, new Object?[]{ events });


        var new_events = (IEnumerable<object>)handle_method.Invoke(instance, new object?[] { command })!;

        return new_events;
    }

    static Command_router()
    {
        // Get all handlers with base type Command_handler (only 1st level base type)
        var handler_types = typeof(Command_router).Assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .Where(t => t.BaseType?.Name == typeof(Command_handler<,>).Name);

        // Add them to the registry: command type -> handler type, apply & handle
        foreach (var handler_type in handler_types)
        {
            var command_type = handler_type.BaseType?.GenericTypeArguments.First();
            var apply_method = handler_type.GetMethod("ApplyAll")!;
            var handle_method = handler_type.GetMethod("Handle")!;
            Command_handlers.Add(command_type!, (handler_type, apply_method, handle_method)); 
        }
    }

    private static readonly Dictionary<Type, 
        (Type Handler_type, MethodInfo Apply_method, MethodInfo Handle_method)> 
        Command_handlers = new();
}

