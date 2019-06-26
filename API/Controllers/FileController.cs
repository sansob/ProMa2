using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogic.Service;
using DataAccess.Context;
using DataAccess.ViewModels;

namespace API.Controllers
{
    public class FileController : ApiController
    {
        private ApplicationContext db = new ApplicationContext();

        public FileController()
        {
        }

        private readonly IFileService _fileService;

        public FileController(IFileService iFileService)
        {
            _fileService = iFileService;
        }

        // GET: api/Projects
        public HttpResponseMessage GetFiles()
        {
            try
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "404 : Data Not Found");
                var result = _fileService.Get();
                if (result != null) message = Request.CreateResponse(HttpStatusCode.OK, result);

                return message;
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500 : Internal Server Error");
            }
        }
        

        // GET: api/Projects/5
        public HttpResponseMessage GetFiles(int id)
        {
            try
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "404 : Data Not Found");
                var result = _fileService.Get(id);
                if (result != null) message = Request.CreateResponse(HttpStatusCode.OK, result);

                return message;
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500 : Internal Server Error");
            }
        }

        // PUT: api/Projects/5
        public HttpResponseMessage PutFile(int id, FileVM fileVm)
        {
            try
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "404 : Data Not Found");
                var result = _fileService.Update(id, fileVm);
                if (result) message = Request.CreateResponse(HttpStatusCode.OK, fileVm);

                return message;
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500 : Internal Server Error");
            }
        }

        // POST: api/Projects
        public HttpResponseMessage InsertProject(FileVM fileVm)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            var result = _fileService.Insert(fileVm);
            if (result) message = Request.CreateResponse(HttpStatusCode.OK, fileVm);

            return message;
        }

        // DELETE: api/Projects/5
        public HttpResponseMessage DeleteProject(int id)
        {
            try
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "404 : Data Not Found");
                var result = _fileService.Delete(id);
                if (result) message = Request.CreateResponse(HttpStatusCode.OK, "200 : OK (Data Deleted)");

                return message;
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500 : Internal Server Error");
            }
        }
    }
}