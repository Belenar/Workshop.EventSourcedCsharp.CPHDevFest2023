namespace BeerSender.Domain.Infrastructure
{
    public interface Command_handler<TCommand>
    {
        IEnumerable<object> Handle(TCommand command);
    }
}
