using System.Text.Json.Serialization;

namespace BeerSender.Domain.Infrastructure;

public abstract record Command([property:JsonIgnore]Guid Aggregate_id);