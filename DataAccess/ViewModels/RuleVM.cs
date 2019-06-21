using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ViewModels
{
    public class RuleVM
    {
        public RuleVM(string rule_name)
        {
            this.Rule_Name = rule_name;
        }
        public RuleVM() { }
        public void Update(int id, string rule_name)
        {
            this.Id = id;
            this.Rule_Name = rule_name;
        }
        public int Id { get; set; }
        public string Rule_Name { get; set; }
    }
}
