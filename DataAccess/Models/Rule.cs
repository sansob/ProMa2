using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base;
using DataAccess.ViewModels;

namespace DataAccess.Models
{
    [Table("TB_M_Rules")]
    public class Rule : BaseModel
    {
        public string Rule_Name { get; set; }
        public Rule(RuleVM ruleVM)
        {
            Rule_Name = ruleVM.Rule_Name;
            CreateDate = DateTimeOffset.Now.ToLocalTime();
        }
        public Rule() { }
        public void Update(RuleVM ruleVM)
        {
            Id = ruleVM.Id;
            Rule_Name = ruleVM.Rule_Name;
            UpdateDate = DateTimeOffset.Now.ToLocalTime();
        }
        public void Delete()
        {
            IsDelete = true;
            DeleteDate = DateTimeOffset.Now.ToLocalTime();
        }
    }
}