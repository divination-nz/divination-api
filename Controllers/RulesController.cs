using Divination.Data.Repository;
using Divination.Models;
using Microsoft.AspNetCore.Mvc;

namespace Divination.Controllers;

[ApiController]
[Route("/divination/rules")]
public class RulesController : ControllerBase
{
    private readonly ILogger<RulesController> _logger;
    private readonly IRulesRepository _repository;

    public RulesController(IRulesRepository repository, ILogger<RulesController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("{index}")]
    public ActionResult<Rule> GetRule(string index)
    {
        var rule = _repository.GetRules().Find(rule => rule.Id == index);

        if (rule == null)
        {
            _logger.LogError($"Rule {index} not found");
            return NotFound();
        }

        return Ok(rule);
    }

    [HttpGet("search")]
    public IEnumerable<Rule> SearchRules(string query)
    {
        // TODO: Implement actual filtering
        return _repository.GetRules();
    }
}