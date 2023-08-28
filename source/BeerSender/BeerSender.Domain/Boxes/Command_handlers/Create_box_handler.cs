using BeerSender.Domain.Infrastructure;

namespace BeerSender.Domain.Boxes.Command_handlers;

internal class Create_box_handler : Command_handler<Create_box, Box_aggregate>
{
    public override IEnumerable<object> Handle(Create_box command)
    {
        var capacity = Box_capacity.From_number_of_spots(command.Number_of_bottles);

        yield return new Box_created(capacity);
    }
}