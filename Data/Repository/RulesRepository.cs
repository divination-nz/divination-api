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
}