using Divination.Models;

namespace Divination.Data.Repository;

public class RulesRepository : IRulesRepository
{
    public RulesRepository()
    {
        var rulesText = ResourceLoader.LoadRulesFromFile();
        var rules = RulesParser.ParseRulesText(rulesText);
        var glossaryTerms = GlossaryParser.ParseGlossary(rulesText);

        using var context = new ApiContext();

        context.Rules.AddRange(rules);
        context.GlossaryTerms.AddRange(glossaryTerms);
        context.SaveChanges();
    }

    public List<Rule> GetRules()
    {
        using var context = new ApiContext();
        return context.Rules.ToList();
    }

    public List<GlossaryTerm> GetGlossaryTerms()
    {
        using var context = new ApiContext();
        return context.GlossaryTerms.ToList();
    }
}