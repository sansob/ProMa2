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

        public bool Insert(RuleVM ruleVM)
        {
            if (string.IsNullOrWhiteSpace(ruleVM.Rule_Name))
            {
                return status;
            }
            else
            {
                return iRuleRepository.Insert(ruleVM);
            }
        }

        public bool Update(int id, RuleVM ruleVM)
        {
            if (string.IsNullOrWhiteSpace(ruleVM.Id.ToString()))
            {
                return status;
            }
            {
                return iRuleRepository.Update(id, ruleVM);
            }
        }
        public bool Delete(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return status;
            }
            else
            {
                return iRuleRepository.Delete(id);
            }
        }
    }
}
