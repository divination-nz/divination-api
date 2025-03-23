using Divination.Models;

namespace Divination.Data.Repository;

public interface IRulesRepository
{
    public List<Rule> GetRules();
    public List<GlossaryTerm> GetGlossaryTerms();
}