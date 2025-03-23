using Divination.Models;

namespace Divination.Data.Repository;

public class RulesRepository : IRulesRepository
{
    public RulesRepository()
    {
        var rulesText = ResourceLoader.LoadRulesFromFile();
        var rules = RulesParser.ParseRulesText(rulesText);

        using var context = new ApiContext();

        context.Rules.AddRange(rules);
        context.SaveChanges();
    }

    public List<Rule> GetRules()
    {
        using var context = new ApiContext();
        return context.Rules.ToList();
    }

    public List<GlossaryTerm> GetGlossaryTerms()
    {
        return new List<GlossaryTerm>
        {
            new()
            {
                Term = "Absorb",
                Description = "A keyword ability that prevents damage. See rule 702.64, “Absorb.”"
            },
            new()
            {
                Term = "Activate",
                Description =
                    "To put an activated ability onto the stack and pay its costs, so that it will eventually resolve and have its effect. See rule 602, “Activating Activated Abilities.”"
            }
        };
    }
}