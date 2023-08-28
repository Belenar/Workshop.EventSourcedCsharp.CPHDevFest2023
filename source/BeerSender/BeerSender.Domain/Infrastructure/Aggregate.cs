namespace BeerSender.Domain.Infrastructure;

public abstract class Aggregate
{
    public void Apply(object @event)
    {
        throw new NotImplementedException();
    }
}

public abstract class Aggregate<TRootEntity> : Aggregate
    where TRootEntity : class, new()
{
    public TRootEntity Root { get; } = new();
}