using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogsAssignment.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class CustomUser
    {
        public Guid UserId { get; set; }
        
        public string Name { get; set; }
    }
}