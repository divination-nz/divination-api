using Divination.Models;
using Microsoft.AspNetCore.Mvc;

namespace Divination.Controllers;

[ApiController]
[Route("/divination/rules")]
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

    [HttpGet("{index}")]
    public Rule GetRule(string index)
    {
        return ExampleRules[0];
    }
    
    [HttpGet("search")]
    public IEnumerable<Rule> SearchRules(string query)
    {
        return ExampleRules;
    }
}