﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MediaCalender.Shared.ContentTypes
{
    public class RarSeries
    {
        public RarSeriesData data { get; set; }
    }
    public class RarSeriesData
    {
        public int id { get; set; }
        public string seriesName { get; set; }
        public List<object> aliases { get; set; }
        public string banner { get; set; }
        public string seriesId { get; set; }
        public string status { get; set; }
        public string firstAired { get; set; }
        public string network { get; set; }
        public string networkId { get; set; }
        public string runtime { get; set; }
        public List<string> genre { get; set; }
        public string overview { get; set; }
        public int lastUpdated { get; set; }
        public string airsDayOfWeek { get; set; }
        public string airsTime { get; set; }
        public string rating { get; set; }
        public string imdbId { get; set; }
        public string zap2itId { get; set; }
        public string added { get; set; }
        public object addedBy { get; set; }
        public double siteRating { get; set; }
        public int siteRatingCount { get; set; }
        public string slug { get; set; }
    }

}
