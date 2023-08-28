namespace BeerSender.Domain.Boxes;

public class Box_aggregate
{
    public Box Root_entity { get; }

    public Box_aggregate(Box root_entity)
    {
        Root_entity = root_entity;
    }

    public void Apply(object @event)
    {
        throw new NotImplementedException();
    }

    public void Apply(Box_closed @event)
    {
        Root_entity.Closed = true;
    }

    public void Apply(Box_failed_to_close @event)
    {
    }

    public void Apply(Bottle_added_to_box @event)
    {
        var existing_bottle = Root_entity.Contents
            .FirstOrDefault(c => c.Bottle == @event.Bottle);

        if (existing_bottle.Bottle is null)
        {
            Root_entity.Contents.Add((@event.Bottle, 1));
        }
        else
        {
            existing_bottle.Quantity++;
        }
    }
}