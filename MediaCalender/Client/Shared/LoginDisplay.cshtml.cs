using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MediaCalender.Shared.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerLoginApp.Shared
{
    public class LoginDisplayBase : ComponentBase
    {
        [Inject]
        protected UserManager<ApplicationUser> UserManager { set; get; }

        [Inject]
        protected IHttpContextAccessor HttpContextAccessor { get; set; }

        public string Name { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var currentUser = await UserManager.GetUserAsync(HttpContextAccessor.HttpContext.User);
            Name = currentUser.FirstName.ToString();
        }
    }
}
