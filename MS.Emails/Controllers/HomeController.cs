using Microsoft.AspNetCore.Mvc;

namespace MS.Emails.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok("funcionando");
        }
    }
}
