using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogic.Service;

namespace API.Controllers
{
    public class GetterTicketController : ApiController
    {
        public GetterTicketController()
        {
        }

        private readonly ITicketService _iTicketService;

        public GetterTicketController(ITicketService iTicketService)
        {
            _iTicketService = iTicketService;
        }

        public HttpResponseMessage GetStatusByProjectId(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            var result = _iTicketService.GetStatusByProjectId(id);
            if (result != null) message = Request.CreateResponse(HttpStatusCode.OK, result);
            return message;
        }
    }
}