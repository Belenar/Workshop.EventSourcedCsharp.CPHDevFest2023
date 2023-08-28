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

        event_config.Property(a => a.Event_Type)
            .HasMaxLength(256)
            .HasColumnType("varchar");

        event_config.Property(a => a.Event_Payload)
            .HasMaxLength(2048)
            .HasColumnType("varchar");

        event_config.Ignore(a => a.Event);
    }
}

public class Aggregate_event
{
    public Guid Aggregate_id { get; set; }
    public int Event_Number { get; set; }
    public string Event_Type { get; set;}
    public string Event_Payload { get; set;}
    public object Event { get;}
}