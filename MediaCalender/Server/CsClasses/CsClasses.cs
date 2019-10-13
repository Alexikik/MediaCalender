using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCalender.Server.CsClasses
{
    public class CsClasses
    {
        public Database database { get; set; }
        public Account account { get; set; }
        public bool loginStatus { get; set; }

        public CsClasses()
        {
            database = new Database();
            account = new Account(database);
            loginStatus = false;
        }
    }
}
