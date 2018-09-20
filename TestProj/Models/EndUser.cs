using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProj.Models
{
    [Table("EndUsers")]
    public class EndUser : User
    {
        public bool IsManager { get; set; }
        public virtual Company Company { get; set; }

        [ForeignKey("Company")]
        public Guid? CompanyId { get; set; }
    }
}