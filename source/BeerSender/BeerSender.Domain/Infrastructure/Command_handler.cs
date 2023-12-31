﻿namespace BeerSender.Domain.Infrastructure;

public abstract class Command_handler<TCommand, TAggregate>
    where TCommand : Command
    where TAggregate : Aggregate, new()
{
    public TAggregate Aggregate { get; } = new ();
    public abstract IEnumerable<object> Handle(TCommand command);

    public void ApplyAll(IEnumerable<object> events)
    {
        foreach (var @event in events)
        {
            Aggregate.Apply((dynamic)@event);
        }
    }
}