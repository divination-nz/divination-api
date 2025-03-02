using Divination.Models;

namespace Divination.Data.Repository;

public interface IRulesRepository
{
    public List<Rule> GetRules();
}