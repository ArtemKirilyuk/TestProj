using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestProj.Models
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        public virtual ICollection<EndUser> EndUsers { get; set; }
        public virtual ICollection<AdminUser> AdminUsers { get; set; }
        public Company()
        {
            EndUsers = new List<EndUser>();
            AdminUsers = new List<AdminUser>();
        }
    }
}