using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base;
using DataAccess.ViewModels;

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
            CreateDate = DateTimeOffset.Now.ToLocalTime();
        }
        public void Update(ProjectMemberVM projectMemberVM)
        {
            Id = projectMemberVM.Id;
            UpdateDate = DateTimeOffset.Now.ToLocalTime();
        }
        public void Delete()
        {
            IsDelete = true;
            DeleteDate = DateTimeOffset.Now.ToLocalTime();
        }
    }
}
