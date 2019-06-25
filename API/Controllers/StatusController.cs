using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogic.Service;
using DataAccess.Context;
using DataAccess.ViewModels;

namespace API.Controllers
{
    public class StatusController : ApiController
    {
        private ApplicationContext db = new ApplicationContext();

        public StatusController()
        {
        }

        private readonly IStatusService _iStatusService;

        public StatusController(IStatusService iStatusService)
        {
            _iStatusService = iStatusService;
        }

        // GET: api/Projects
        public HttpResponseMessage GetStatus()
        {
            try
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "404 : Data Not Found");
                var result = _iStatusService.Get();
                if (result != null) message = Request.CreateResponse(HttpStatusCode.OK, result);

                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500 : Internal Server Error");
            }
        }

        public HttpResponseMessage GetStatusByModule(string modulQuery)
        {
            try
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "404 : Data Not Found");
                var result = _iStatusService.GetStatusByModule(modulQuery);
                if (result != null) message = Request.CreateResponse(HttpStatusCode.OK, result);

                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500 : Internal Server Error");
            }
        }

        // GET: api/Projects/5
        public HttpResponseMessage GetStatus(int id)
        {
            try
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "404 : Data Not Found");
                var result = _iStatusService.Get(id);
                if (result != null) message = Request.CreateResponse(HttpStatusCode.OK, result);

                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500 : Internal Server Error");
            }
        }

        // PUT: api/Projects/5
        public HttpResponseMessage PutStatus(int id, StatusVM statusVm)
        {
            try
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "404 : Data Not Found");
                var result = _iStatusService.Update(id, statusVm);
                if (result) message = Request.CreateResponse(HttpStatusCode.OK, statusVm);

                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500 : Internal Server Error");
            }
        }

        // POST: api/Projects
        public HttpResponseMessage InsertStatus(StatusVM statusVm)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            var result = _iStatusService.Insert(statusVm);
            if (result) message = Request.CreateResponse(HttpStatusCode.OK, statusVm);

            return message;
        }

        // DELETE: api/Projects/5
        public HttpResponseMessage DeleteStatus(int id)
        {
            try
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "404 : Data Not Found");
                var result = _iStatusService.Delete(id);
                if (result) message = Request.CreateResponse(HttpStatusCode.OK, "200 : OK (Data Deleted)");

                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500 : Internal Server Error");
            }
        }
    }
}