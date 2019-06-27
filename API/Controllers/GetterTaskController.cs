using BusinessLogic.Service;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class GetterTaskController : ApiController
    {
        public GetterTaskController() { }
        private readonly ITaskService _iTaskService;
        public GetterTaskController(ITaskService taskService)
        {
            _iTaskService = taskService;
        }

        public HttpResponseMessage GetTaskByProjectId(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            var result = _iTaskService.GetTaskByProjectId(id);
            if (result != null) message = Request.CreateResponse(HttpStatusCode.OK, result);
            return message;
        }
    }
}