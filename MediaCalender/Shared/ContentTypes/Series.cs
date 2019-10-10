using System;
using System.Collections.Generic;
using System.Text;

namespace MediaCalender.Shared.ContentTypes
{
    class Series : MediaContent
    {
        public string name { get; set; }
        public Series(string name) : base(name) 
        { }
    }
}
