using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCalender.Server.CsClasses
{
    public class MediaLibary
    {
        public Database database { get; set; }

        public MediaLibary(Database database)
        {
            this.database = database;
        }
    }
}
