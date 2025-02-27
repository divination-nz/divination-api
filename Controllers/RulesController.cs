using Divination.Models;
using Microsoft.AspNetCore.Mvc;

namespace Divination.Controllers;

[ApiController]
[Route("[controller]")]
public class RulesController : ControllerBase
{
    private static readonly Rule[] ExampleRules =
    {
        new()
        {
            Index = "100.1",
            Description = "Blah"
        },
        new()
        {
            Index = "100.2",
            Description = "Blah"
        }
    };

    private readonly ILogger<RulesController> _logger;

    public RulesController(ILogger<RulesController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetRules")]
    public IEnumerable<Rule> Get()
    {
        return ExampleRules;
    }
}