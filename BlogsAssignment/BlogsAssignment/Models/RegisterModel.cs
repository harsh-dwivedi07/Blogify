using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogsAssignment.Models
{
    public class RegisterModel
    {
        public Guid Id { get; set; }
        [Required]
        public String UserName { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public String Password { get; set; }

    }
}