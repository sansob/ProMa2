using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogic.Service;
using DataAccess.Context;

namespace API.Controllers
{
    public class GetterFileController : ApiController
    {
        public GetterFileController()
        {
            
        }
        private ApplicationContext db = new ApplicationContext();

        private readonly IFileService _fileService;

        public GetterFileController(IFileService iFileService)
        {
            _fileService = iFileService;
        }

        
        // GET
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "404 : Data Not Found");
                var result = _fileService.GetFileByProject(id);
                if (result != null) message = Request.CreateResponse(HttpStatusCode.OK, result);

                return message;
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500 : Internal Server Error");
            }
        }

    }
}