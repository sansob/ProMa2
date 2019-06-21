using DataAccess.Models;
using DataAccess.ViewModels;
using System.Collections.Generic;

namespace Common.Repository
{
    public interface IRuleRepository
    {
        List<Rule> Get();
        List<Rule> GetSearch(string values);
        Rule Get(int id);
        bool insert(RuleVM ruleVM);
        bool update(int id, RuleVM ruleVM);
        bool delete(int id);
    }
}
