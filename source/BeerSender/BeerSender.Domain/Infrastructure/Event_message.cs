namespace BeerSender.Domain.Infrastructure;

public record Event_message(
    Guid Aggregate_id,
    int Event_number,
    object Event);