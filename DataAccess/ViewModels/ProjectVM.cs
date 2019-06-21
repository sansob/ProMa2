using System;

namespace DataAccess.ViewModels {
    public class ProjectVM {
        public int Id { get; set; }
        public string Project_name { get; set; }
        public DateTimeOffset Project_Start { get; set; }
        public DateTimeOffset Project_Deadline { get; set; }
        public string Project_Detail { get; set; }
        public int Status_Id { get; set; }

        public ProjectVM() {}

        public ProjectVM(string projectName, DateTimeOffset projectStart, DateTimeOffset projectDeadline, string projectDetail, int statusId) {
            this.Project_name = projectName;
            this.Project_Start = projectStart;
            this.Project_Deadline = projectDeadline;
            this.Project_Detail = projectDetail;
            this.Status_Id = statusId;
        }

        public void Update(string projectName, DateTimeOffset projectStart, DateTimeOffset projectDeadline, string projectDetail, int statusId) {
            this.Project_name = projectName;
            this.Project_Start = projectStart;
            this.Project_Deadline = projectDeadline;
            this.Project_Detail = projectDetail;
            this.Status_Id = statusId;
        }
    }
}