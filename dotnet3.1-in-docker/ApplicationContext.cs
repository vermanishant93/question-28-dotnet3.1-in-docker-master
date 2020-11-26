using dotnet3._1_in_docker.Model;
using dotnet3._1_in_docker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet3._1_in_docker
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
       
    }
}
