namespace BeerSender.Tests;

public class Close_box_test : Box_test
{
    [Fact]
    public void When_box_is_empty_should_not_close()
    {
        Given();

        When(
            Close_box());

        Expect(
            Close_failed_because_box_was_empty());
    }

    [Fact]
    public void When_box_is_not_empty_should_close()
    {
        Given(
            Beer1_added());

        When(
            Close_box());

        Expect(
            Box_closed());
    }
}