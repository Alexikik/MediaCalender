using System;
using System.Collections.Generic;
using System.Text;

namespace MediaCalender.Shared.ContentTypes
{
    public class Episode
    {
        public int Id { get; set; }
        public string Director { get; set; }
        public string Title { get; set; }
        public int EpisodeNumber { get; set; }
        public int SeasonNumber { get; set; }
        public string firstAired { get; set; }
        public string GuestStars { get; set; }
        public string ImdbId { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public int RatingCount { get; set; }
        public string Writers { get; set; }
        public string EpisodeImage { get; set; }
        public int LastUpdated { get; set; }
        public int SeasonId { get; set; }
        public int SeriesId { get; set; }
        public int ThumbHeight { get; set; }
        public int ThumbWidth { get; set; }
        public string TmsExport { get; set; }
    }
}
