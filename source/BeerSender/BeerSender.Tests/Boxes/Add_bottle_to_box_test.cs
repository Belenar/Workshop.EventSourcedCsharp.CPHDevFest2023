namespace BeerSender.Tests.Boxes;

public class Add_bottle_to_box_test : Box_test
{
    [Fact]
    public void When_spots_available_adds_bottle()
    {
        Given(
            Box_created_with_capacity(2),
            Beer1_added_to_box());

        When(
            Add_beer1_to_box());

        Expect(
            Beer1_added_to_box());
    }

    [Fact]
    public void When_box_full_fails_to_add()
    {
        Given(
            Box_created_with_capacity(1),
            Beer1_added_to_box());

        When(
            Add_beer1_to_box());

        Expect(
            Beer_failed_to_add_because_box_full());
    }
}