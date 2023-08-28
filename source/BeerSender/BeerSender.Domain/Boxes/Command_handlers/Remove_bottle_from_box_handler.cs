using BeerSender.Domain.Infrastructure;

namespace BeerSender.Domain.Boxes.Command_handlers;

internal class Remove_bottle_from_box_handler : Command_handler<Remove_bottle_from_box, Box_aggregate>
{
    public override IEnumerable<object> Handle(Remove_bottle_from_box command)
    {
        var box = Aggregate.Root;
        var bottle = new Beer_bottle(command.Brewery, command.Name, command.Alcohol_percentage);
        
        if (box.Contents.Any(c => c.Bottle == bottle && c.Quantity > 0))
            yield return new Bottle_removed_from_box(bottle);
        else
            yield return new Bottle_failed_to_remove(
                Bottle_failed_to_remove.Fail_reason.Bottle_not_found);
    }
}