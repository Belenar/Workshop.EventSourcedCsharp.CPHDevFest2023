namespace BeerSender.Tests.Boxes;

public class Create_box_test : Box_test
{
    [Theory]
    [InlineData(6, 6)]
    [InlineData(12, 12)]
    [InlineData(24, 24)]
    public void When_created_with_valid_capacity_returns_same_capacity(
        int requested_capacity,
        int actual_capacity)
    {
        Given();

        When(
            Create_box_from_capacity(requested_capacity));

        Expect(
            Box_created_with_capacity(actual_capacity));
    }

    [Theory]
    [InlineData(0, 6)]
    [InlineData(1, 6)]
    [InlineData(5, 6)]
    [InlineData(7, 12)]
    [InlineData(11, 12)]
    [InlineData(13, 24)]
    [InlineData(23, 24)]
    public void When_created_with_invalid_capacity_returns_correct_capacity(
        int requested_capacity,
        int actual_capacity)
    {
        Given();

        When(
            Create_box_from_capacity(requested_capacity));

        Expect(
            Box_created_with_capacity(actual_capacity));
    }

}