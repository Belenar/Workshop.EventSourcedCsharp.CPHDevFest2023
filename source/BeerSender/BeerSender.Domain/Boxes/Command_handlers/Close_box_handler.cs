using BeerSender.Domain.Infrastructure;

namespace BeerSender.Domain.Boxes.Command_handlers
{
    internal class Close_box_handler : Command_handler<Close_box>
    {
        private readonly Box _root_entity;

        public Close_box_handler(Box root_entity)
        {
            _root_entity = root_entity;
        }

        public IEnumerable<object> Handle(Close_box command)
        {
            if (_root_entity.Contents.Any(c => c.Quantity > 0))
                yield return new Box_closed();
            else
                yield return new Box_failed_to_close(
                    Box_failed_to_close.Reason.Box_has_no_bottles);
        }
    }
}
