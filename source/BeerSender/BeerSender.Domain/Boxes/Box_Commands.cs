using BeerSender.Domain.Infrastructure;

namespace BeerSender.Domain.Boxes;

public record Create_box(int Number_of_bottles);

public record Close_box(Guid Box_id) : Command(Box_id);