using System.Collections.Generic;
using System.Web.Http;
using DataAccess.Context;
using DataAccess.Models;
using BusinessLogic.Service;
using DataAccess.ViewModels;
using System.Net.Http;
using System.Net;

namespace API.Controllers
{
    public class ProjectMembersController : ApiController
    {
        private ApplicationContext db = new ApplicationContext();
        public ProjectMembersController() { }
        private readonly IProjectMemberService iProjectMemberService;
        public ProjectMembersController(IProjectMemberService _iProjectMemberService)
        {
            iProjectMemberService = _iProjectMemberService;
        }
        // GET: api/ProjectMembers
        public HttpResponseMessage GetProjectMembers()
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "NotFound");
            var result = iProjectMemberService.Get();
            if (result != null)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return message;
        }

        // GET: api/ProjectMembers/5
        public HttpResponseMessage GetProjectMember(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "NotFound");
            var result = iProjectMemberService.Get(id);
            if (result != null)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return message;
        }

        // PUT: api/ProjectMembers/5
        public HttpResponseMessage PutProjectMember(int id, ProjectMemberVM projectMemberVM)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotModified, "Not Modified");
            var result = iProjectMemberService.Update(id, projectMemberVM);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK);
            }
            return message;
        }
        // POST: api/ProjectMembers
        public HttpResponseMessage InsertProjectMember(ProjectMemberVM projectMemberVM)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            var result = iProjectMemberService.Insert(projectMemberVM);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK);
            }
            return message;
        }
        // DELETE: api/ProjectMembers/5
        public HttpResponseMessage DeleteProjectMember(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Content");
            var result = iProjectMemberService.Delete(id);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK);
            }
            return message;
        }
    }
}