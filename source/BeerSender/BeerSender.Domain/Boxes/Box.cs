namespace BeerSender.Domain.Boxes
{
    public class Box
    {
        public Box(Box_capacity capacity)
        {
            Capacity = capacity;
        }
        public Box_capacity Capacity { get; }

        public Shipping_label Label { get; set; }

        public List<(Beer_bottle Bottle, int Quantity)> Contents { get; } =
            new ();

    }

    public record Box_capacity(int Number_of_spots)
    {
        public static Box_capacity From_number_of_spots(int number_of_spots)
        {
            var capacity = number_of_spots switch
            {
                <= 6 => new Box_capacity(6),
                <= 12 => new Box_capacity(12),
                _ => new Box_capacity(24)
            };
            return capacity;
        }
    }

    public record Beer_bottle(string Brewery, string Name, decimal Alcohol_percentage);

    public record Shipping_label(Carrier Carrier, string Tracking_code)
    {
        public bool Is_valid()
        {
            return Carrier switch
            {
                Carrier.UPS => Tracking_code.StartsWith("ABC"), // UPS logic
                Carrier.FedEx => Tracking_code.StartsWith("DEF"), // UPS logic
                Carrier.PostNord => Tracking_code.StartsWith("GHI"), // UPS logic
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public enum Carrier
    {
        UPS,
        FedEx,
        PostNord
    }

}
