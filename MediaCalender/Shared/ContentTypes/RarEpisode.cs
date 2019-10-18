using System;
using System.Collections.Generic;
using System.Text;

namespace MediaCalender.Shared.ContentTypes
{
    public class RarEpisode
    {
        //public Links links { get; set; }
        public List<RarEpisodeData> data { get; set; }

        public Episode convertToEpisode()
        {
            Episode episode = new Episode();
            string temp;
            int intTemp;
            bool result;

            episode.id = this.data[0].id;
            episode.airedSeason = this.data[0].airedSeason;
            episode.airedSeasonID = this.data[0].airedSeasonID;
            episode.airedEpisodeNumber = this.data[0].airedEpisodeNumber;
            episode.episodeName = this.data[0].episodeName;
            episode.firstAired = this.data[0].firstAired;

            if (this.data[0].guestStars.Count > 0)
            {
                temp = this.data[0].guestStars[0].ToString();
                for (int i = 1; i < this.data[0].guestStars.Count - 1; i++)
                {
                    temp += ", " + this.data[0].guestStars[i];
                }
                episode.guestStars = temp;
            }
            else
                episode.guestStars = "";
            episode.director = this.data[0].director;

            if (this.data[0].directors.Count > 0)
            {
                temp = this.data[0].directors[0].ToString();
                for (int i = 1; i < this.data[0].directors.Count - 1; i++)
                {
                    temp += ", " + this.data[0].directors[i];
                }
                episode.directors = temp;
            }
            else
                episode.directors = "";

            if (this.data[0].writers.Count > 0)
            {
                temp = this.data[0].writers[0].ToString();
                for (int i = 1; i < this.data[0].writers.Count - 1; i++)
                {
                    temp += ", " + this.data[0].writers[i];
                }
                episode.writers = temp;
            }
            else
                episode.writers = "";
            episode.overview = this.data[0].overview;
            episode.productionCode = this.data[0].productionCode;
            episode.showUrl = this.data[0].showUrl;

            //result = Int32.TryParse(this.data[0].absoluteNumber, out intTemp);
            //if (result)
            //    movie.Metascore = intTemp;
            //else
            //    movie.Metascore = 0;
            episode.absoluteNumber = this.data[0].absoluteNumber;
            episode.filename = this.data[0].filename;
            episode.seriesId = this.data[0].seriesId;
            episode.lastUpdatedBy = this.data[0].lastUpdatedBy;
            episode.thumbAuthor = this.data[0].thumbAuthor;
            episode.thumbAdded = this.data[0].thumbAdded;
            episode.thumbWidth = this.data[0].thumbWidth;
            episode.thumbHeight = this.data[0].thumbHeight;
            episode.imdbId = this.data[0].imdbId;
            episode.siteRating = this.data[0].siteRating;
            episode.siteRatingCount = this.data[0].siteRatingCount;


            return episode;
        }
    }
    //public class Links
    //{
    //    public int first { get; set; }
    //    public int last { get; set; }
    //    public object next { get; set; }
    //    public object prev { get; set; }
    //}

    //public class Language
    //{
    //    public string episodeName { get; set; }
    //    public string overview { get; set; }
    //}

    public class RarEpisodeData
    {
        public int id { get; set; }
        public int airedSeason { get; set; }
        public int airedSeasonID { get; set; }
        public int airedEpisodeNumber { get; set; }
        public string episodeName { get; set; }
        public string firstAired { get; set; }
        public List<object> guestStars { get; set; }
        public string director { get; set; }
        public List<object> directors { get; set; }
        public List<object> writers { get; set; }
        public string overview { get; set; }
        //public Language language { get; set; }
        public string productionCode { get; set; }
        public string showUrl { get; set; }
        public int lastUpdated { get; set; }
        public string dvdDiscid { get; set; }
        public int dvdSeason { get; set; }
        public int dvdEpisodeNumber { get; set; }
        public object dvdChapter { get; set; }
        public int absoluteNumber { get; set; }
        public string filename { get; set; }
        public int seriesId { get; set; }
        public int lastUpdatedBy { get; set; }
        public object airsAfterSeason { get; set; }
        public object airsBeforeSeason { get; set; }
        public object airsBeforeEpisode { get; set; }
        public int thumbAuthor { get; set; }
        public string thumbAdded { get; set; }
        public string thumbWidth { get; set; }
        public string thumbHeight { get; set; }
        public string imdbId { get; set; }
        public int siteRating { get; set; }
        public int siteRatingCount { get; set; }

        
    }
}
