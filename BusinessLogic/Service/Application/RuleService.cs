using System;
using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.ViewModels;
using Common.Repository;

namespace BusinessLogic.Service.Application
{
    public class RuleService : IRuleService
    {
        public RuleService() { }
        bool status = false;
        private readonly IRuleRepository iRuleRepository;
        public RuleService(IRuleRepository _iRuleRepository)
        {
            iRuleRepository = _iRuleRepository;
        }
        public bool delete(int id)
        {
            return iRuleRepository.delete(id);
        }

        public List<Rule> Get()
        {
            return iRuleRepository.Get();
        }

        public Rule Get(int id)
        {
            return iRuleRepository.Get(id);
        }

        public List<Rule> GetSearch(string values)
        {
            return iRuleRepository.GetSearch(values);
        }

        public bool insert(RuleVM ruleVM)
        {
            if (string.IsNullOrWhiteSpace(ruleVM.Rule_Name))
            {
                return status;
            }
            else
            {
                return iRuleRepository.insert(ruleVM);
            }
        }

        public bool update(int id, RuleVM ruleVM)
        {
            return iRuleRepository.update(id, ruleVM);
        }
    }
}
