using BeerSender.Domain.Boxes;
using BeerSender.Domain.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerSender.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        [HttpPost]
        public IActionResult CloseBox([FromBody] Command command)
        {
            HandleCommand(command);
            return Ok();
        }

        internal void HandleCommand(Command command)
        {

        }
    }
}
