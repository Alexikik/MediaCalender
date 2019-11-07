using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCalender.Server.CsClasses
{
    public class MediaLibary
    {
        public DatabaseOLD database { get; set; }

        public MediaLibary(DatabaseOLD database)
        {
            this.database = database;
        }
    }
}
