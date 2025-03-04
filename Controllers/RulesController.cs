using System.Text.RegularExpressions;
using Divination.Data.Repository;
using Divination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Divination.Controllers;

[ApiController]
[EnableRateLimiting("fixed")]
[Route("/rules")]
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
    public ActionResult<List<Rule>> SearchRules(string query)
    {
        var rules = _repository.GetRules();
        var matchedRules = new List<Rule>();

        foreach (var rule in rules)
            if (Regex.IsMatch(rule.Description.ToLower(), @$"\b{query.ToLower()}\b"))
                matchedRules.Add(rule);

        if (matchedRules.Count == 0) return NotFound();

        return Ok(matchedRules);
    }
}