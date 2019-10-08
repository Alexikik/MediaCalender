using System;
using System.Collections.Generic;
using System.Text;

namespace MediaCalender.Shared.Containers
{
    public class LoginCredentials
    {
        public string username { get; set; }
        public string password { get; set; }

        public LoginCredentials(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
