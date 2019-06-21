using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccess.Context;
using DataAccess.Models;
using BusinessLogic.Service;

namespace API.Controllers
{
    public class ProjectMembersController : ApiController
    {
        private ApplicationContext db = new ApplicationContext();
        public ProjectMembersController() { }
        bool status = false;
        private readonly IProjectMemberService iProjectMemberService;
        public ProjectMembersController(IProjectMemberService _iProjectMemberService)
        {
            iProjectMemberService = _iProjectMemberService;
        }
        // GET: api/ProjectMembers
        public List<ProjectMember> GetProjectMembers()
        {
            return iProjectMemberService.Get();
        }

        // GET: api/ProjectMembers/5
        [ResponseType(typeof(ProjectMember))]
        public IHttpActionResult GetProjectMember(int id)
        {
            ProjectMember projectMember = db.ProjectMembers.Find(id);
            if (projectMember == null)
            {
                return NotFound();
            }

            return Ok(projectMember);
        }

        // PUT: api/ProjectMembers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProjectMember(int id, ProjectMember projectMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectMember.Id)
            {
                return BadRequest();
            }

            db.Entry(projectMember).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectMemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProjectMembers
        [ResponseType(typeof(ProjectMember))]
        public IHttpActionResult PostProjectMember(ProjectMember projectMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectMembers.Add(projectMember);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = projectMember.Id }, projectMember);
        }

        // DELETE: api/ProjectMembers/5
        [ResponseType(typeof(ProjectMember))]
        public IHttpActionResult DeleteProjectMember(int id)
        {
            ProjectMember projectMember = db.ProjectMembers.Find(id);
            if (projectMember == null)
            {
                return NotFound();
            }

            db.ProjectMembers.Remove(projectMember);
            db.SaveChanges();

            return Ok(projectMember);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectMemberExists(int id)
        {
            return db.ProjectMembers.Count(e => e.Id == id) > 0;
        }
    }
}