using BeerSender.Web.Read_store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeerSender.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxOverviewController : ControllerBase
    {
        private readonly Read_context _db_context;

        public BoxOverviewController(Read_context db_context)
        {
            _db_context = db_context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Box_overview>>> Get()
        {
            var response = await _db_context.Box_overviews
                .Where(o => o.Status != Box_status.Sent)
                .ToListAsync();

            return Ok(response);
        }
    }
}
