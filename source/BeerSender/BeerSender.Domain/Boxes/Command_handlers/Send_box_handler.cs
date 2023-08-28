using BeerSender.Domain.Infrastructure;

namespace BeerSender.Domain.Boxes.Command_handlers;

internal class Send_box_handler : Command_handler<Send_box, Box_aggregate>
{
    public override IEnumerable<object> Handle(Send_box command)
    {
        if (Aggregate.Root.Closed == false)
            yield return new Box_failed_to_send(Box_failed_to_send.Fail_reason.Box_was_not_closed);
        else if (Aggregate.Root.Label is null)
            yield return new Box_failed_to_send(Box_failed_to_send.Fail_reason.Label_was_not_added);
        else
            yield return new Box_sent();
    }
}