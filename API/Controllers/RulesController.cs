using System.Web.Http;
using DataAccess.Context;
using System.Collections.Generic;
using BusinessLogic.Service;
using DataAccess.Models;
using DataAccess.ViewModels;
using System.Net.Http;
using System.Net;

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
        public HttpResponseMessage GetRules()
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            var result = iRuleService.Get();
            if (result != null)
            {
                message = Request.CreateResponse(HttpStatusCode.OK,result);
            }
            return message;
        }

        // GET: api/Rules/5
        public HttpResponseMessage GetRule(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            var result = iRuleService.Get(id);
            if (result!=null)
            {
                message = Request.CreateResponse(HttpStatusCode.OK,result);
            }
            return message;
        }
        // PUT: api/Rules/5
        public HttpResponseMessage PutRule(int id, RuleVM ruleVM)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotModified, "Not Modified");
            var result = iRuleService.Update(id, ruleVM);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK,result, "Update successfully");
            }
            return message;
        }
        // POST: api/Rules
        public HttpResponseMessage InsertRule(RuleVM ruleVM)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            var result = iRuleService.Insert(ruleVM);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, "Insert successfully");
            }
            return message;
        }
        // DELETE: api/Rules/5
        public HttpResponseMessage DeleteRule(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Content");
            var result = iRuleService.Delete(id);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, "Delete successfully");
            }
            return message;
        }
    }
}