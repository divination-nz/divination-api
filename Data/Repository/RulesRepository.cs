using Divination.Models;

namespace Divination.Data.Repository;

public class RulesRepository : IRulesRepository
{
    public RulesRepository()
    {
        var rulesText = ResourceLoader.LoadRulesFromFile();
        Console.WriteLine(rulesText);

        using var context = new ApiContext();
        var rules = new List<Rule>
        {
            new()
            {
                Id = "100.1",
                Description =
                    "These Magic rules apply to any Magic game with two or more players, including two-player games and multiplayer games."
            },
            new()
            {
                Id = "100.1a",
                Description = "A two-player game is a game that begins with only two players."
            },
            new()
            {
                Id = "100.1b",
                Description =
                    "A multiplayer game is a game that begins with more than two players. See section 8, “Multiplayer Rules.”"
            }
        };

        context.Rules.AddRange(rules);
        context.SaveChanges();
    }

    public List<Rule> GetRules()
    {
        using var context = new ApiContext();
        return context.Rules.ToList();
    }
}