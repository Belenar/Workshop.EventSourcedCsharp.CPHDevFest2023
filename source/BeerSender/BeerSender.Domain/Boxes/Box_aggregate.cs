using BeerSender.Domain.Infrastructure;

namespace BeerSender.Domain.Boxes;

public class Box_aggregate : Aggregate<Box>
{
    public void Apply(Box_created @event)
    {
        Root.Capacity = @event.Capacity;
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

    public void Apply(Bottle_failed_to_add @event)
    {
    }

    public void Apply(Bottle_removed_from_box @event)
    {
        var existing_bottle = Root.Contents
            .FirstOrDefault(c => c.Bottle == @event.Bottle);

        if (existing_bottle.Bottle is not null)
        {
            if (existing_bottle.Quantity > 1)
                existing_bottle.Quantity--;
            else
                Root.Contents.Remove(existing_bottle);
        }
    }

    public void Apply(Bottle_failed_to_remove @event)
    {
    }

    public void Apply(Label_added_to_box @event)
    {
        Root.Label = @event.Label;
    }

    public void Apply(Label_failed_to_add @event)
    {
    }

    public void Apply(Box_closed @event)
    {
        Root.Closed = true;
    }

    public void Apply(Box_failed_to_close @event)
    {
    }

    public void Apply(Box_sent @event)
    {
        Root.Sent = true;
    }

    public void Apply(Box_failed_to_send @event)
    {
    }
}