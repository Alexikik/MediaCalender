using System;
using System.Collections.Generic;
using System.Text;

namespace MediaCalender.Shared.ContentTypes
{
    class Movie : MediaContent
    {
        public string name { get; set; }

        public Movie(string name) : base(name)
        { }
    }
}
