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
    public class TasksController : ApiController
    {
        private ApplicationContext db = new ApplicationContext();
        public TasksController() { }
        ITaskService iTaskService;
        public TasksController(ITaskService _iTaskService)
        {
            iTaskService = _iTaskService;
        }
        // GET: api/Tasks
        public HttpResponseMessage GetTasks()
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotModified, "Not Modified");
            var result = iTaskService.Get();
            if (result != null)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return message;
        }

        // GET: api/Tasks/5
        public HttpResponseMessage GetTask(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotModified, "Not Modified");
            var result = iTaskService.Get(id);
            if (result!=null)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return message;
        }

        // GET: api/Tasks/5
        public HttpResponseMessage GetTaskByProjectId(int project_id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotModified, "Not Modified");
            var result = iTaskService.Get(project_id);
            if (result != null)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return message;
        }

        // PUT: api/Tasks/5
        public HttpResponseMessage PutTasks(int id, TaskVM taskVM)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotModified, "Not Modified");
            var result = iTaskService.Update(id, taskVM);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK);
            }
            return message;
        }
        // POST: api/Tasks
        public HttpResponseMessage InsertTask(TaskVM taskVM)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            var result = iTaskService.Insert(taskVM);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK);
            }
            return message;
        }
        // DELETE: api/Tasks/5
        public HttpResponseMessage DeleteTask(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Content");
            var result = iTaskService.Delete(id);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK);
            }
            return message;
        }
    }
}