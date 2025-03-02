using System.Text.RegularExpressions;
using Divination.Models;

namespace Divination.Data;

public class RulesParser
{
    public static List<Rule> ParseRulesText(string rulesText)
    {
        var creditsText = "Credits";
        var rules = new List<Rule>();
        var rulesTrimmed =
            rulesText.Substring(rulesText.IndexOf(creditsText, StringComparison.Ordinal) + creditsText.Length);

        string[] rulesArray = rulesTrimmed.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        foreach (var rule in rulesArray)
        {
            if (rule.Contains("Glossary")) break;

            var ruleSplit = rule.Split(" ", 2);

            if (!IsValidRuleId(ruleSplit[0])) continue;

            rules.Add(new Rule
            {
                Id = CleanRuleId(ruleSplit[0]),
                Description = ruleSplit[1]
            });
        }

        return rules;
    }

    /*
     * Checks if a given rule id is valid
     *
     * Allows:
     * - 101.1
     * - 101.1a
     * Disallows:
     * - 1.
     * - 101.
     * - Example
     */
    private static bool IsValidRuleId(string id)
    {
        return Regex.IsMatch(id, "[0-9]{3}[.0-9a-z]{2,3}");
    }

    // Removes trailing dot for rule ids
    private static string CleanRuleId(string id)
    {
        if (id.EndsWith(".")) return id.Substring(0, id.Length - 1);

        return id;
    }
}