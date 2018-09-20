﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestProj.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}