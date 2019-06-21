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
            this.Status_name = statusVm.Status_name;
            this.Status_module = statusVm.Status_module;
            this.CreateDate = DateTimeOffset.Now.LocalDateTime;
        }
    }
}