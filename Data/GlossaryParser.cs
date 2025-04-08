using System.Text.RegularExpressions;
using Divination.Models;

namespace Divination.Data;

public class GlossaryParser
{
    public static List<GlossaryTerm> ParseGlossary(string rulesText)
    {
        var creditsText = "Credits";
        var glossaryText = "Glossary";
        var glossaryTerms = new List<GlossaryTerm>();

        var creditsPos = rulesText.IndexOf(creditsText, StringComparison.Ordinal);
        var glossaryTrimmed =
            rulesText.Substring(rulesText.IndexOf(glossaryText, creditsPos, StringComparison.Ordinal) +
                                glossaryText.Length);

        // To enable the use of \r to determine the split between glossary entries
        var glossaryWithNewlines = glossaryTrimmed.Replace("\r", "\r\n");
        var glossaryArray = glossaryWithNewlines.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        
        /*
         * Quick overview of how this works:
         * - Skip through blank lines until non-empty text is found; consider that the glossary term
         * - When we have a glossary term, all the text we find gets added to the description
         * - The next time we find a blank line, consider that the end of the 'block'
         * - Commit the term and description to the list, then reset both, and continue
         */
        var currentTerm = "";
        var currentDescription = "";
        foreach (var glossary in glossaryArray)
        {
            // Stop once the Credits section is reached (i.e. end of glossary)
            if (glossary.Trim() == creditsText)
            {
                break;
            }

            // Skip through blank lines
            if (glossary == "\r" && currentTerm.Length == 0)
            {
                continue;
            }

            // Stop at end of block and save to list before resetting
            if (glossary == "\r" && currentDescription.Length > 0)
            {
                glossaryTerms.Add(new GlossaryTerm()
                {
                    Term = currentTerm,
                    Description = currentDescription.Replace("\r", Environment.NewLine),
                });
                currentTerm = "";
                currentDescription = "";
            }
            else if (currentTerm.Length > 0)
            {
                currentDescription += glossary;
            }
            else
            {
                currentTerm = glossary.Trim();
            }
        }

        return glossaryTerms;
    }
}