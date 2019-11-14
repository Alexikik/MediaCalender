using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MediaCalender.Server.LoginSystem
{
    public class LoginSystemDatabase : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public LoginSystemDatabase(DbContextOptions<LoginSystemDatabase> options)
            : base(options)
        {
        }
    }
}
