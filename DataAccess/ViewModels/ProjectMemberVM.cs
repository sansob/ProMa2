using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ViewModels
{
    public class ProjectMemberVM
    {
        public ProjectMemberVM(int project_id, string username, int user_id, int rule_id)
        {
            this.Project_Id = project_id;
            this.User_Id = user_id;
            this.User_Name = username;
            this.Rule_Id = rule_id;
        }
        public ProjectMemberVM() { }
        public void Update(int id, int project_id, string username, int user_id, int rule_id)
        {
            this.Id = id;
            this.Project_Id = project_id;
            this.User_Id = user_id;
            this.User_Name = username;
            this.Rule_Id = rule_id;
        }
        public int Id { get; set; }
        public int Project_Id { get; set; }
        public int User_Id { get; set; }
        public int Rule_Id { get; set; }
        public string User_Name { get; set; }
    }
}
