using BeerSender.Domain.Infrastructure;

namespace BeerSender.Domain.Boxes;

public record Create_box(Guid Box_id, int Number_of_bottles): Command(Box_id);

public record Add_bottle_to_box(
    Guid Box_id, 
    string brewery,
    string name,
    decimal alcohol_percentage) : Command(Box_id);

public record Remove_bottle_from_box(
    Guid Box_id,
    string brewery,
    string name,
    decimal alcohol_percentage) : Command(Box_id);

public record Add_label_to_box(
    Guid Box_id,
    Carrier carrier,
    string tracking_code) : Command(Box_id);

public record Close_box(Guid Box_id) : Command(Box_id);

public record Send_box(Guid Box_id) : Command(Box_id);