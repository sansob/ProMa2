using Core.Base;
using DataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    [Table("TB_T_Project_Members")]
    public class ProjectMember: BaseModel
    {
        [ForeignKey("Project")]
        public int Project_Id { get; set; }
        public int User_Id { get; set; }
        [ForeignKey("Rule")]
        public int Rule_Id { get; set; }

        public Project Project { get; set; }
        public Rule Rule { get; set; }

        public ProjectMember() { }
        public ProjectMember(ProjectMemberVM projectMemberVM)
        {
            this.CreateDate = DateTimeOffset.Now.ToLocalTime();
        }
        public void Update(int id, ProjectMemberVM projectMemberVM)
        {
            this.Id = id;
            this.UpdateDate = DateTimeOffset.Now.ToLocalTime();
        }
        public void Delete()
        {
            this.IsDelete = true;
            this.DeleteDate = DateTimeOffset.Now.ToLocalTime();
        }
    }
}
