using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCalender.Shared.ContentTypes;
using MediaCalender.Shared.Containers;
using MediaCalender.Shared;

namespace MediaCalender.Server.CsClasses
{
    public class LoginSystem
    {
        // Database database { get; }
        public bool loginStatus { get; set; }
        

        public LoginSystem()
        {
            
        }
        
        // Logs users in
        public bool Login(LoginCredentials credentials, Database database)
        {
            loginStatus = database.Users.Any(u => (u.Username == credentials.username) && (u.Password == credentials.password));
            return loginStatus;
        }
    }
}
