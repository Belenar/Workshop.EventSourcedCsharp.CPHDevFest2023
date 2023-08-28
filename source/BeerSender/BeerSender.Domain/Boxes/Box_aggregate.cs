using BeerSender.Domain.Infrastructure;

namespace BeerSender.Domain.Boxes;

public class Box_aggregate : Aggregate<Box>
{
    public void Apply(Box_closed @event)
    {
        Root.Closed = true;
    }

    public void Apply(Box_failed_to_close @event)
    {
    }

    public void Apply(Bottle_added_to_box @event)
    {
        var existing_bottle = Root.Contents
            .FirstOrDefault(c => c.Bottle == @event.Bottle);

        if (existing_bottle.Bottle is null)
        {
            Root.Contents.Add((@event.Bottle, 1));
        }
        else
        {
            existing_bottle.Quantity++;
        }
    }
}