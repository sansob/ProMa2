using Core.Base;
using DataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    [Table("TB_M_Rules")]
    public class Rule : BaseModel
    {
        public string Rule_Name { get; set; }
        public Rule(RuleVM ruleVM)
        {
            this.Rule_Name = ruleVM.Rule_Name;
            this.CreateDate = DateTimeOffset.Now.ToLocalTime();
        }
        public Rule() { }
        public void Update(RuleVM ruleVM)
        {
            this.Id = ruleVM.Id;
            this.UpdateDate = DateTimeOffset.Now.ToLocalTime();
        }
        public void Delete()
        {
            this.IsDelete = true;
            this.DeleteDate = DateTimeOffset.Now.ToLocalTime();
        }
    }
}