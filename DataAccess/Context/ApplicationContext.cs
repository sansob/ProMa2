using System.Data.Entity;
using DataAccess.Models;

namespace DataAccess.Context {
    public class ApplicationContext : DbContext {
        public ApplicationContext() : base("ApplicationContext") {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<File> Files { get; set; }
    }
}