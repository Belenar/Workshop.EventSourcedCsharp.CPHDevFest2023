namespace BeerSender.Tests.Boxes;

public class Remove_bottle_from_box_test : Box_test
{
    [Fact]
    public void When_bottle_is_added_removes_bottle()
    {
        Given(
            Box_created_with_capacity(2),
            Beer1_added_to_box());

        When(
            Remove_beer1_from_box());

        Expect(
            Beer1_removed_from_box());
    }

    [Fact]
    public void When_bottle_not_added_fails_to_remove()
    {
        Given(
            Box_created_with_capacity(2),
            Beer2_added_to_box());

        When(
            Remove_beer1_from_box());

        Expect(
            Beer_failed_to_remove_because_not_found());
    }
}