using Microsoft.AspNetCore.Mvc;

namespace PasswordManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<PasswordsController> _logger;

        public PasswordsController(ILogger<PasswordsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<Password> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Password
            {
                ID = index
            })
            .ToArray();
        }
    }
}