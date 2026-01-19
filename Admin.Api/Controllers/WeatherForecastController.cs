using Admin.Api.Controllers.Base;
using Common.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class WeatherForecastController : AppBaseApiController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    //var userId = UserId;
        //    var rng = new Random();
        //    var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        //UserId = UserId,
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //    var s = ApiResponse<WeatherForecast[]>.Success(data);
        //    s.StatusCode = 200;
        //    return Ok(s);
        //}
        public async Task<IActionResult> Get()
        {
            try
            {
                //var userId = UserId;
                var rng = new Random();
                var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
                return Ok(SuccessMessage(data));
            }
            catch (Exception ex)
            {
                AppLogging.LogException(ex.Message); return BadRequest(ErrorMessage(ex.Message));
            }

        }
        [HttpGet]
        [Route("GetException")]
        public async Task<IActionResult> GetException(string message)
        {
            try
            {
                var amenities = message;

                return Ok(SuccessMessage(amenities));
            }
            catch (Exception ex)
            {
                AppLogging.LogException(ex.Message); return BadRequest(ErrorMessage(ex.Message));
            }
            // throw new Exception(message);
        }
        [HttpGet]
        [Route("GetExceptionSystem")]
        public async Task<IActionResult> GetExceptionSystem()
        {
            try
            {
                throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                AppLogging.LogException(ex.Message); return BadRequest(ErrorMessage(ex.Message));
            }
        }
        public class WeatherForecast
        {
            public DateTime Date { get; set; }

            public int TemperatureC { get; set; }
            //public string? UserId { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

            public string Summary { get; set; }
        }
    }
}
