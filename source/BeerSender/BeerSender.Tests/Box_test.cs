using BeerSender.Domain.Boxes;

namespace BeerSender.Tests;

public class Box_test : Command_Test
{
    private Guid _box_id = Guid.NewGuid();
    private Beer_bottle _beer1 = 
        new Beer_bottle("Gouden Carolus", "Quadruple Whisky Infused", 12.7M);

    #region Commands

    protected Close_box Close_box()
    {
        return new Close_box(_box_id);
    }

    #endregion

    #region Events

    protected Bottle_added_to_box Beer1_added()
    {
        return new Bottle_added_to_box(_beer1);
    }

    protected Box_closed Box_closed()
    {
        return new Box_closed();
    }

    protected Box_failed_to_close Close_failed_because_box_was_empty()
    {
        return new Box_failed_to_close(Box_failed_to_close.Reason.Box_has_no_bottles);
    }

    #endregion
}