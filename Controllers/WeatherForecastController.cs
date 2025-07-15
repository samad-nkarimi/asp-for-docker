using AspForDocker.AspDbContext;
using Microsoft.AspNetCore.Mvc;

namespace AspForDocker.Controllers;

[ApiController]
[Route("api/")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly AppDbContext _dbContext;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet(Name = "get")]
    [Route("get")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 2).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet(Name = "new")]
    [Route("new")]
    public IActionResult GetNew()
    {
        int x=_dbContext.Users.Count();
        _dbContext.Users.Add(new global::User(){Id=x,Name="new user"});
        _dbContext.SaveChanges();
        return Ok($"{x}");
    }
}

