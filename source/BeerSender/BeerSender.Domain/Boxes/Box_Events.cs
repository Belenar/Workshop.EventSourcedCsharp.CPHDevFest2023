namespace BeerSender.Domain.Boxes;

public record Box_created(Box_capacity Capacity);

public record Box_closed;

public record Box_failed_to_close(Box_failed_to_close.Fail_reason Reason)
{
    public enum Fail_reason
    {
        Box_has_no_bottles
    }
}

public record Bottle_added_to_box(Beer_bottle Bottle);

public record Bottle_failed_to_add(Bottle_failed_to_add.Fail_reason Reason)
{
    public enum Fail_reason
    {
        Box_was_full
    }
}

public record Label_added_to_box(Shipping_label Label);

public record Label_failed_to_add(Label_failed_to_add.Fail_reason Reason)
{
    public enum Fail_reason
    {
        Invalid_tracking_code
    }
}

public record Bottle_removed_from_box(Beer_bottle Bottle);

public record Bottle_failed_to_remove(Bottle_failed_to_remove.Fail_reason Reason)
{
    public enum Fail_reason
    {
        Bottle_not_found
    }
}

public record Box_sent;

public record Box_failed_to_send(Box_failed_to_send.Fail_reason Reason)
{
    public enum Fail_reason
    {
        Box_was_not_closed,
        Label_was_not_added
    }
}