using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccess.Context;
using System.Collections.Generic;
using BusinessLogic.Service;


namespace API.Controllers
{
    public class RulesController : ApiController
    {
        private ApplicationContext db = new ApplicationContext();
        public RulesController() { }
        private readonly IRuleService iRuleService;
        public RulesController(IRuleService _iRuleService)
        {
            iRuleService = _iRuleService;
        }
        // GET: api/Rules
        public List<DataAccess.Models.Rule> GetRules()
        {
            return iRuleService.Get();
        }

        // GET: api/Rules/5
        [ResponseType(typeof(DataAccess.Models.Rule))]
        public IHttpActionResult GetRule(int id)
        {
            DataAccess.Models.Rule rule = db.Rules.Find(id);
            if (rule == null)
            {
                return NotFound();
            }

            return Ok(rule);
        }

        // PUT: api/Rules/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRule(int id, DataAccess.Models.Rule rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rule.Id)
            {
                return BadRequest();
            }

            db.Entry(rule).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rules
        [ResponseType(typeof(DataAccess.Models.Rule))]
        public IHttpActionResult PostRule(DataAccess.Models.Rule rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rules.Add(rule);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rule.Id }, rule);
        }

        // DELETE: api/Rules/5
        [ResponseType(typeof(DataAccess.Models.Rule))]
        public IHttpActionResult DeleteRule(int id)
        {
            DataAccess.Models.Rule rule = db.Rules.Find(id);
            if (rule == null)
            {
                return NotFound();
            }

            db.Rules.Remove(rule);
            db.SaveChanges();

            return Ok(rule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RuleExists(int id)
        {
            return db.Rules.Count(e => e.Id == id) > 0;
        }
    }
}