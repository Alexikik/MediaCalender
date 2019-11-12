using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MediaCalender.Shared
{
    public class User
    {
        public int Key { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        //public ICollection<int> FollowedSeriesKeys { get; set; } = new List<int>();
        //public ICollection<int> FollowedMovieKeys { get; set; } = new List<int>();
    }
}
