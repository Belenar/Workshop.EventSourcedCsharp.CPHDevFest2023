using BeerSender.Domain.Infrastructure;
using FluentAssertions;

namespace BeerSender.Tests
{
    public class Command_Test
    {
        private List<object> _events = new();
        private List<object> _new_events = new();

        protected void Given(params object[] events)
        {
            _events.AddRange(events);
        }

        protected void When(Command command)
        {
            var router = new Command_router(
                _ => _events,
                (_, @event) => _new_events.Add(@event));

            router.Handle_command(command);
        }

        protected void Expect(params object[] events)
        {
            events.Length.Should().Be(_new_events.Count);
            for (int i = 0; i < events.Length; i++)
            {
                var expected = events[i];
                var actual = _new_events[i];
                expected.Should().Be(actual);
            }
        }
    }
}