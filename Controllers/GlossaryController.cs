using System.Text.RegularExpressions;
using Divination.Data.Repository;
using Divination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Divination.Controllers;

[ApiController]
[EnableRateLimiting("fixed")]
[Route("/divination/glossary")]
public class GlossaryController : ControllerBase
{
    private readonly ILogger<GlossaryController> _logger;
    private readonly IRulesRepository _repository;

    public GlossaryController(IRulesRepository repository, ILogger<GlossaryController> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    [HttpGet]
    public ActionResult<List<GlossaryTerm>> GetTerms() {
        var glossaryTerms = _repository.GetGlossaryTerms();
        
        return Ok(glossaryTerms);
    }

    [HttpGet("search")]
    public ActionResult<List<GlossaryTerm>> SearchGlossary(string query)
    {
        var glossary = _repository.GetGlossaryTerms();
        var matchedTerms = new List<GlossaryTerm>();

        foreach (var term in glossary)
            if (Regex.IsMatch(term.Description.ToLower(), @$"\b{query.ToLower()}\b") || 
                Regex.IsMatch(term.Term.ToLower(), @$"\b{query.ToLower()}\b"))
                matchedTerms.Add(term);

        if (matchedTerms.Count == 0) return NotFound();

        return Ok(matchedTerms);
    }
}