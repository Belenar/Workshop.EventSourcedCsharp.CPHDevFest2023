using BeerSender.Domain.Boxes;

namespace BeerSender.Tests.Boxes;

public class Send_box_test : Box_test
{
    [Fact]
    public void When_box_meets_requirements_is_sent()
    {
        Given(
            Box_created_with_capacity(6),
            Beer1_added_to_box(),
            Box_closed(),
            Label_with_carrier_and_code_added(Carrier.UPS, "ABC_123"));

        When(
            Send_box());

        Expect(
            Box_sent());
    }

    [Fact]
    public void When_box_not_closed_fails()
    {
        Given(
            Box_created_with_capacity(6),
            Beer1_added_to_box(),
            Label_with_carrier_and_code_added(Carrier.UPS, "ABC_123"));

        When(
            Send_box());

        Expect(
            Send_failed_because_not_closed());
    }

    [Fact]
    public void When_no_label_added_fails()
    {
        Given(
            Box_created_with_capacity(6),
            Beer1_added_to_box(),
            Box_closed());

        When(
            Send_box());

        Expect(
            Send_failed_because_no_label());
    }
}