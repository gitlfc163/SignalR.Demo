using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.ChatClient.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHubContext hubContext;

        public WeatherForecastController(IHubContext hubContext,ILogger<WeatherForecastController> logger)
        {
            this.hubContext = hubContext;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Send(string userName, string message)
        {
            hubContext.Clients.All.SendAsync("ReceivePublicMessage", userName, message);
            return Ok();
        }
    }
}