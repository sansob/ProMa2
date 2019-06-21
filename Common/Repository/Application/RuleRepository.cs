﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.ViewModels;
using DataAccess.Context;
using System.Data.Entity;

namespace Common.Repository.Application
{
    public class RuleRepository : IRuleRepository
    {
        ApplicationContext applicationContext = new ApplicationContext();
        bool status = false;
        public bool delete(int id)
        {
            var get = Get(id);
            if (get != null)
            {
                get.Delete();
                applicationContext.Entry(get).State = EntityState.Modified;
                var result = applicationContext.SaveChanges();
                return result > 0;
            }
            else
            {
                return false;
            }
        }

        public List<Rule> Get()
        {
            var get = applicationContext.Rules.Where(x => x.IsDelete == false).ToList();
            return get;
        }

        public Rule Get(int id)
        {
            var get = applicationContext.Rules.Find(id);
            return get;
        }

        public List<Rule> GetSearch(string values)
        {
            var get = applicationContext.Rules.Where(x => (x.Id.ToString().Contains(values) || x.Rule_Name.Contains(values) ) && x.IsDelete == false).ToList();
            return get;
        }

        public bool insert(RuleVM ruleVM)
        {
            var push = new Rule(ruleVM);
            applicationContext.Rules.Add(push);
            var result = applicationContext.SaveChanges();
            if (result > 0)
            {
                status = true;
                return status;
            }
            else
            {
                return status;
            }
        }

        public bool update(int id, RuleVM ruleVM)
        {
            var get = Get(id);
            if (get != null)
            {
                get.Update(id, ruleVM);
                applicationContext.Entry(get).State = EntityState.Modified;
                applicationContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
