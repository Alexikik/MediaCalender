using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCalender.Shared.ContentTypes;
using MediaCalender.Shared.Containers;

namespace MediaCalender.Server.CsClasses
{
    public class Account
    {
        public string name { get; set; }
        public bool loginStatus { get; set; }
        List<MediaContent>followedContent { get; set; }
        Database database { get; set; }

        public Account(Database database)
        {
            //this.name = name;
            //this.followedContent = followedContent;
            this.database = database;
        }


        public void Login(LoginCredentials credentials)
        {
            bool loginStatus;
            if (database.CheckLoginCredentials(credentials))
                loginStatus = true;
            else
                loginStatus = false;

            this.loginStatus = loginStatus;


        }
    }
}
