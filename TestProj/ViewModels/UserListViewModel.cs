using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestProj.Models;

namespace TestProj.ViewModels
{
    public class UserListViewModel
    {
        public List<AdminUser> AdminUsers { get; set; }
        public List<EndUser> EndUsers { get; set; }
        public UserListViewModel()
        {
            AdminUsers = new List<AdminUser>();
            EndUsers = new List<EndUser>();
        }
    }
}