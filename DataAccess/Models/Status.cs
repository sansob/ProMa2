using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base;
using DataAccess.ViewModels;

namespace DataAccess.Models {
    [Table("TB_M_Status")]
    public class Status : BaseModel {
        public string Status_name { get; set; }
        public string Status_module { get; set; }

        public Status() {
        }

        public Status(StatusVM statusVm) {
            Status_name = statusVm.Status_name;
            Status_module = statusVm.Status_module;
            CreateDate = DateTimeOffset.Now.LocalDateTime;
        }

        public void Update(StatusVM statusVm)
        {
            Status_name = statusVm.Status_name;
            Status_module = statusVm.Status_module;
            UpdateDate = DateTimeOffset.Now.LocalDateTime;
        }
        public void Delete() {
            IsDelete = true;
            DeleteDate = DateTimeOffset.Now.LocalDateTime;
        }
    }
}