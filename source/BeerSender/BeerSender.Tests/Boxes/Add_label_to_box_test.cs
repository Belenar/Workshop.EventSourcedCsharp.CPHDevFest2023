using BeerSender.Domain.Boxes;

namespace BeerSender.Tests.Boxes;

public class Add_label_to_box_test : Box_test
{
    [Fact]
    public void When_code_is_valid_adds_label()
    {
        Given();

        When(
            Add_label_with_carrier_and_code(Carrier.UPS, "ABC_123"));

        Expect(
            Label_with_carrier_and_code_added(Carrier.UPS, "ABC_123"));
    }

    [Fact]
    public void When_code_is_invalide_fails_to_add()
    {
        Given();

        When(
            Add_label_with_carrier_and_code(Carrier.UPS, "XXX_123"));

        Expect(
            Label_failed_to_add_because_invalid_code());
    }
}