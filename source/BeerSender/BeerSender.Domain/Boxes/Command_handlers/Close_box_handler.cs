using BeerSender.Domain.Infrastructure;

namespace BeerSender.Domain.Boxes.Command_handlers;

internal class Close_box_handler : Command_handler<Close_box, Box_aggregate>
{
    public override IEnumerable<object> Handle(Close_box command)
    {
        if (Aggregate.Root.Contents.Any(c => c.Quantity > 0))
            yield return new Box_closed();
        else
            yield return new Box_failed_to_close(
                Box_failed_to_close.Fail_reason.Box_has_no_bottles);
    }
}


