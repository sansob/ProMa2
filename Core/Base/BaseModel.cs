using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Base {
    public class BaseModel {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset? CreateDate { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
        public DateTimeOffset? DeleteDate { get; set; }
        public bool IsDelete { get; set; }
    }
}