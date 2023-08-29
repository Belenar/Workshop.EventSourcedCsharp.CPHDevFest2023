using BeerSender.Domain.Boxes;
using BeerSender.Domain.Infrastructure;
using BeerSender.Web.Read_store;

namespace BeerSender.Web.Projections;

public class Box_overview_projection : Projection
{
    private readonly Read_context _db_context;
    public int Batch_size => 100;
    public TimeSpan Wait_time => TimeSpan.FromSeconds(5);

    public Type[] Event_types => new []
    {
        typeof(Box_created),
        typeof(Bottle_added_to_box),
        typeof(Bottle_removed_from_box),
        typeof(Box_closed),
        typeof(Box_sent)
    };

    public Box_overview_projection(Read_context db_context)
    {
        _db_context = db_context;
    }

    public async Task Process_Events(IEnumerable<Event_message> events)
    {
        foreach (var event_message in events)
        {
            switch (event_message.Event)
            {
                case Box_created created:
                    await _db_context.Box_overviews.AddAsync(
                        new Box_overview
                        {
                            Box_id = event_message.Aggregate_id,
                            Open_spaces = created.Capacity.Number_of_spots,
                            Status = Box_status.Open
                        });
                    break;
                case Bottle_added_to_box:
                    var bottle_add_overview = await _db_context.Box_overviews
                        .FindAsync(event_message.Aggregate_id);
                    if(bottle_add_overview is not null)
                        bottle_add_overview.Open_spaces--;
                    break;
                case Bottle_removed_from_box:
                    var bottle_remove_overview = await _db_context.Box_overviews
                        .FindAsync(event_message.Aggregate_id);
                    if (bottle_remove_overview is not null)
                        bottle_remove_overview.Open_spaces++;
                    break;
                case Box_closed:
                    var box_closed_overview = await _db_context.Box_overviews
                        .FindAsync(event_message.Aggregate_id);
                    if (box_closed_overview is not null)
                        box_closed_overview.Status = Box_status.Closed;
                    break;
                case Box_sent:
                    var box_sent_overview = await _db_context.Box_overviews
                        .FindAsync(event_message.Aggregate_id);
                    if (box_sent_overview is not null)
                        box_sent_overview.Status = Box_status.Sent;
                    break;
            }
        }

        await _db_context.SaveChangesAsync();
    }
}