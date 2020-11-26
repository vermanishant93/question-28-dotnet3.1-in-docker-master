using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet3._1_in_docker.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string Birthdate { get; set; }
        public string Password { get; set; }
    }
}
