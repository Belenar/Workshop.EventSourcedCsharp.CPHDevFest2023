using BeerSender.Domain.Infrastructure;

namespace BeerSender.Domain.Boxes.Command_handlers;

internal class Add_bottle_to_box_handler : Command_handler<Add_bottle_to_box, Box_aggregate>
{
    public override IEnumerable<object> Handle(Add_bottle_to_box command)
    {
        var box = Aggregate.Root;
        var bottle = new Beer_bottle(command.Brewery, command.Name, command.Alcohol_percentage);

        if (box.Contents.Sum(c => c.Quantity) < box.Capacity?.Number_of_spots)
            yield return new Bottle_added_to_box(bottle);
        else
            yield return new Bottle_failed_to_add(
                Bottle_failed_to_add.Fail_reason.Box_was_full);
    }
}