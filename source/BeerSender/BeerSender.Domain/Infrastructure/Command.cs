namespace BeerSender.Domain.Infrastructure;

public abstract record Command(Guid Aggregate_id);