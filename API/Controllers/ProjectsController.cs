using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogic.Service;
using DataAccess.Context;
using DataAccess.ViewModels;

namespace API.Controllers {
    public class ProjectsController : ApiController {
        private ApplicationContext db = new ApplicationContext();

        public ProjectsController() {
        }

        private readonly IProjectService _iProjectService;

        public ProjectsController(IProjectService iProjectService) {
            _iProjectService = iProjectService;
        }

        // GET: api/Projects
        public HttpResponseMessage GetProjects() {
            try {
                var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "404 : Data Not Found");
                var result = _iProjectService.Get();
                if (result != null) {
                    message = Request.CreateResponse(HttpStatusCode.OK, result);
                }

                return message;
            }
            catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500 : Internal Server Error");
            }
        }

        // GET: api/Projects/5
        public HttpResponseMessage GetProject(int id) {
            try {
                var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "404 : Data Not Found");
                var result = _iProjectService.Get(id);
                if (result != null) {
                    message = Request.CreateResponse(HttpStatusCode.OK, result);
                }

                return message;
            }
            catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500 : Internal Server Error");
            }
        }

        // PUT: api/Projects/5
        public HttpResponseMessage PutProject(int id, ProjectVM projectVm) {
            try {
                var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "404 : Data Not Found");
                var result = _iProjectService.Update(id, projectVm);
                if (result) {
                    message = Request.CreateResponse(HttpStatusCode.OK, projectVm);
                }

                return message;
            }
            catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500 : Internal Server Error");
            }
        }

        // POST: api/Projects
        public HttpResponseMessage InsertProject(ProjectVM projectVm) {
            var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            var result = _iProjectService.Insert(projectVm);
            if (result) {
                message = Request.CreateResponse(HttpStatusCode.OK, projectVm);
            }

            return message;
        }

        // DELETE: api/Projects/5
        public HttpResponseMessage DeleteProject(int id) {
            try {
                var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "404 : Data Not Found");
                var result = _iProjectService.Delete(id);
                if (result) {
                    message = Request.CreateResponse(HttpStatusCode.OK, "200 : OK (Data Deleted)");
                }

                return message;
            }
            catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "500 : Internal Server Error");
            }
        }
    }
}