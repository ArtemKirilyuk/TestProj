using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProj.Models
{
    [Table("AdminUsers")]
    public class AdminUser : User
    {
        public bool IsActive { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public AdminUser()
        {
            Companies = new List<Company>();
        }
    }
}