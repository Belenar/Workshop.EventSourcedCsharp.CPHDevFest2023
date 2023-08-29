using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace BeerSender.Web.Event_store;

public class Event_context : DbContext
{
    public Event_context(DbContextOptions<Event_context> options): base(options)
    { }

    public DbSet<Aggregate_event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var event_config = modelBuilder.Entity<Aggregate_event>();

        event_config.HasKey(a => new { a.Aggregate_id, a.Event_Number });

        event_config.HasIndex(a => a.Row_version);

        event_config.Property(a => a.Event_Type)
            .HasMaxLength(256)
            .HasColumnType("varchar");

        event_config.Property(a => a.Event_Payload)
            .HasMaxLength(2048)
            .HasColumnType("varchar");

        event_config.Property(a => a.Row_version)
            .IsRowVersion();

        event_config.Ignore(a => a.Event);
    }
}

public class Aggregate_event
{
    public Guid Aggregate_id { get; set; }
    public int Event_Number { get; set; }
    public string Event_Type { get; set;}
    public string Event_Payload { get; set;}
    public byte[] Row_version { get; set; }

    private object? _event;
    public object Event
    {
        get
        {
            if (_event is null)
            {
                // Risky: if event namespaces move, this may break
                var type = Type.GetType(Event_Type);
                _event = JsonSerializer.Deserialize(Event_Payload, type!);
            }

            return _event!;
        }
        set
        {
            _event = value;
            Event_Type = _event.GetType().AssemblyQualifiedName!;
            Event_Payload = JsonSerializer.Serialize(_event);
        }
    }
}
public static class EntityFrameworkHelper
{
    public static int Compare(this byte[] b1, byte[] b2)
    {
        throw new Exception("This method can only be used in EF LINQ Context");
    }
}