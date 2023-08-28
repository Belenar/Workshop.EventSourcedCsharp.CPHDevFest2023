using BeerSender.Domain.Infrastructure;

namespace BeerSender.Domain.Boxes.Command_handlers;

internal class Add_label_to_box_handler : Command_handler<Add_label_to_box, Box_aggregate>
{
    public override IEnumerable<object> Handle(Add_label_to_box command)
    {
        var label = new Shipping_label(command.Carrier, command.Tracking_code);

        if (label.Is_valid())
            yield return new Label_added_to_box(label);
        else
            yield return new Label_failed_to_add(
                Label_failed_to_add.Fail_reason.Invalid_tracking_code);

    }
}