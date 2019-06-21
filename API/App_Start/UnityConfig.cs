using BusinessLogic.Service;
using BusinessLogic.Service.Application;
using Common.Repository;
using Common.Repository.Application;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IProjectRepository, ProjectRepository>();
            container.RegisterType<IStatusRepository, StatusRepository>();
            container.RegisterType<IReplyRepository, ReplyRepository>();
            container.RegisterType<ITicketRepository, TicketRepository>();
            container.RegisterType<IRuleRepository, RuleRepository>();
            container.RegisterType<ITaskRepository, TaskRepository>();
            container.RegisterType<IProjectMemberRepository, ProjectMemberRepository>();

            //this is for service 
            container.RegisterType<IProjectService, ProjectService>();
            container.RegisterType<IStatusService, StatusService>();
            container.RegisterType<IReplyService, ReplyService>();
            container.RegisterType<ITicketService, TicketService>();
            container.RegisterType<IRuleService, RuleService>();
            container.RegisterType<ITaskService, TaskService>();
            container.RegisterType<IProjectMemberService, ProjectMemberService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}