using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogic.Service;

namespace API.Controllers
{
    public class ProjectMmController: ApiController
    {
        public ProjectMmController()
        {
        }

        private readonly IProjectMemberService _projectMemberService;

        public ProjectMmController(IProjectMemberService projectMemberService)
        {
            _projectMemberService = projectMemberService;
        }

        public HttpResponseMessage Get(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            var result = _projectMemberService.GetProjectMemberByProjectId(id);
            if (result != null) message = Request.CreateResponse(HttpStatusCode.OK, result);
            return message;
        }
    }

}