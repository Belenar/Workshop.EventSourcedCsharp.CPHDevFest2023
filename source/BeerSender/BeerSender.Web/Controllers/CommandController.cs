
using BeerSender.Domain.Infrastructure;
using BeerSender.Web.Event_store;
using Microsoft.AspNetCore.Mvc;

namespace BeerSender.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly Command_router _router;
        private readonly Event_service _event_service;

        public CommandController(Command_router router, Event_service event_service)
        {
            _router = router;
            _event_service = event_service;
        }

        [HttpPost]
        public IActionResult PostCommand([FromBody] Command command)
        {
            _router.Handle_command(command);
            _event_service.Commit();
            return Ok();
        }
    }
}
