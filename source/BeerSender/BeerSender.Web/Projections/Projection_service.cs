using BeerSender.Domain.Infrastructure;
using BeerSender.Web.Event_store;
using BeerSender.Web.Read_store;
using Microsoft.EntityFrameworkCore;

namespace BeerSender.Web.Projections;

public class Projection_service<TProjection>: BackgroundService
where TProjection : class, Projection
{
    private readonly IServiceProvider _service_provider;

    public Projection_service(IServiceProvider service_provider)
    {
        _service_provider = service_provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var checkpoint = await Get_checkpoint();

        while (!stoppingToken.IsCancellationRequested)
        {
            // Create Scope & DB contexts
            var scope = _service_provider.CreateScope();
            var read_context = scope.ServiceProvider.GetRequiredService<Read_context>();
            var event_context = scope.ServiceProvider.GetRequiredService<Event_context>();

            // Create projection
            var projection = scope.ServiceProvider.GetRequiredService<TProjection>();

            // Fetch a batch of events
            var events = await Get_batch(event_context, projection, checkpoint);

            if (events.Any())
            {
                // Process events
                await using var transaction = await read_context.Database.BeginTransactionAsync(stoppingToken);

                await projection.Process_Events(events.Select(a =>
                    new Event_message(a.Aggregate_id, a.Event_Number, a.Event)));

                // Update checkpoint
                checkpoint = events.Max(ev => ev.Row_version_long);
                Save_checkpoint(read_context, checkpoint);

                // Commit transaction
                await transaction.CommitAsync(stoppingToken);
            }

            // Wait if needed
            if (events.Count < projection.Batch_size)
            {
                await Task.Delay(projection.Wait_time, stoppingToken);
            }
        }
    }

    private void Save_checkpoint(Read_context read_context, long checkpoint)
    {
        var new_checkpoint = new Projection_checkpoint
        {
            Projection_type = typeof(TProjection).Name,
            Checkpoint = checkpoint
        };
        read_context.Checkpoints.Update(new_checkpoint);
        read_context.SaveChanges();
    }

    private async Task<List<Aggregate_event>> Get_batch(
        Event_context event_context,
        TProjection projection,
        long checkpoint)
    {
        var type_list = projection.Event_types
            .Select(t => t.AssemblyQualifiedName)
            .ToList();

        var events = await event_context.Events
            .Where(e => type_list.Contains(e.Event_Type))
            .Where(e => e.Row_version_long > checkpoint)
            .OrderBy(e => e.Row_version)
            .Take(projection.Batch_size)
            .ToListAsync();

        return events;
    }

    private async Task<long> Get_checkpoint()
    {
        var scope = _service_provider.CreateScope();
        var read_context = scope.ServiceProvider.GetRequiredService<Read_context>();

        var projection_name = typeof(TProjection).Name;
        var checkpoint = await read_context.Checkpoints.FindAsync(projection_name);

        if (checkpoint is null)
        {
            checkpoint = new Projection_checkpoint
            {
                Projection_type = projection_name,
                Checkpoint = 0
            };
            read_context.Checkpoints.Add(checkpoint);
            await read_context.SaveChangesAsync();
        }

        return checkpoint.Checkpoint;
    }
}

public interface Projection
{
    int Batch_size { get; }
    TimeSpan Wait_time { get; }
    Type[] Event_types { get; }

    Task Process_Events(IEnumerable<Event_message> events);
}