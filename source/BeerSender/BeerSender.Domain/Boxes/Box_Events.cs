namespace BeerSender.Domain.Boxes;

public record Box_created(Box_capacity Capacity);

public record Box_closed();

public record Box_failed_to_close(Box_failed_to_close.Reason reason)
{
    public enum Reason
    {
        Box_has_no_bottles
    }
};

public record Bottle_added_to_box(Beer_bottle Bottle);