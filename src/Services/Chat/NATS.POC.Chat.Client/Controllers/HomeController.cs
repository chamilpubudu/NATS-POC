using Microsoft.AspNetCore.Mvc;

namespace NATS.POC.Chat.Client.Controllers
{
    [Route("")]
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get() => new RedirectResult("~/swagger");
    }
}
