using DataAccess.Models;
using DataAccess.ViewModels;
using System.Collections.Generic;

namespace BusinessLogic.Service
{
    public interface IRuleService
    {
        List<Rule> Get();
        List<Rule> GetSearch(string values);
        Rule Get(int id);
        bool Insert(RuleVM ruleVM);
        bool Update(int id, RuleVM ruleVM);
        bool Delete(int id);
    }
}
