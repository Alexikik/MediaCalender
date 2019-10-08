using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCalender.Server.CsClasses
{
    public class CsClasses
    {
        public MediaLibary mediaLibary { get; set; }
        public Account account { get; set; }
        Database database { get; set; }
        public bool loginStatus { get; set; }

        public CsClasses()
        {
            database = new Database();
            mediaLibary = new MediaLibary(database);
            account = new Account(database);
            loginStatus = false;
        }
    }
}
