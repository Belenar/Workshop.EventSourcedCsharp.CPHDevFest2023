using BeerSender.Domain.Boxes;

namespace BeerSender.Tests.Boxes;

public class Box_test : Command_Test
{
    private Guid _box_id = Guid.NewGuid();
    private Beer_bottle _beer1 =
        new Beer_bottle("Gouden Carolus", "Quadruple Whisky Infused", 12.7M);

    private Beer_bottle _beer2 =
        new Beer_bottle("Beer 4 Nature", "Hert", 6.7M);

    #region Commands

    protected Create_box Create_box_from_capacity(int number_of_bottles)
    {
        return new Create_box(_box_id, number_of_bottles);
    }

    protected Add_bottle_to_box Add_beer1_to_box()
    {
        return new Add_bottle_to_box(_box_id, _beer1.Brewery, _beer1.Name, _beer1.Alcohol_percentage);
    }

    protected Add_label_to_box Add_label_with_carrier_and_code(Carrier carrier, string tracking_code)
    {
        return new Add_label_to_box(_box_id, carrier, tracking_code);
    }

    protected Remove_bottle_from_box Remove_beer1_from_box()
    {
        return new Remove_bottle_from_box(_box_id, _beer1.Brewery, _beer1.Name, _beer1.Alcohol_percentage);
    }

    protected Close_box Close_box()
    {
        return new Close_box(_box_id);
    }

    protected Send_box Send_box()
    {
        return new Send_box(_box_id);
    }

    #endregion

    #region Events

    protected Box_created Box_created_with_capacity(int number_of_bottles)
    {
        return new Box_created(new Box_capacity(number_of_bottles));
    }

    protected Bottle_added_to_box Beer1_added_to_box()
    {
        return new Bottle_added_to_box(_beer1);
    }

    protected Bottle_added_to_box Beer2_added_to_box()
    {
        return new Bottle_added_to_box(_beer2);
    }

    protected Bottle_failed_to_add Beer_failed_to_add_because_box_full()
    {
        return new Bottle_failed_to_add(Bottle_failed_to_add.Fail_reason.Box_was_full);
    }

    protected Label_added_to_box Label_with_carrier_and_code_added(Carrier carrier, string tracking_code)
    {
        return new Label_added_to_box(new Shipping_label(carrier, tracking_code));
    }

    protected Label_failed_to_add Label_failed_to_add_because_invalid_code()
    {
        return new Label_failed_to_add(Label_failed_to_add.Fail_reason.Invalid_tracking_code);
    }

    protected Bottle_removed_from_box Beer1_removed_from_box()
    {
        return new Bottle_removed_from_box(_beer1);
    }

    protected Bottle_failed_to_remove Beer_failed_to_remove_because_not_found()
    {
        return new Bottle_failed_to_remove(
            Bottle_failed_to_remove.Fail_reason.Bottle_not_found);
    }

    protected Box_closed Box_closed()
    {
        return new Box_closed();
    }

    protected Box_failed_to_close Close_failed_because_box_was_empty()
    {
        return new Box_failed_to_close(Box_failed_to_close.Fail_reason.Box_has_no_bottles);
    }

    protected Box_sent Box_sent()
    {
        return new Box_sent();
    }

    protected Box_failed_to_send Send_failed_because_not_closed()
    {
        return new Box_failed_to_send(Box_failed_to_send.Fail_reason.Box_was_not_closed);
    }

    protected Box_failed_to_send Send_failed_because_no_label()
    {
        return new Box_failed_to_send(Box_failed_to_send.Fail_reason.Label_was_not_added);
    }

    #endregion
}