using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCalender.Shared.Containers;
using MediaCalender.Shared.ContentTypes;
using Microsoft.EntityFrameworkCore;

namespace MediaCalender.Server.CsClasses
{
    public class SeriesLibary
    {
        //Database database;
        //SeriesLibary seriesLibary;
        //public SeriesLibary(Database database, SeriesLibary seriesLibary)
        //{
        //    this.database = database;
        //    this.seriesLibary = seriesLibary;
        //}

        public async Task<ResultContainer> AddSeries(string seriesName, Database database)
        {
            Series series;
            ResultContainer result = new ResultContainer();
            GetImdbSeries getter = new GetImdbSeries();
            int seriesId;

            // Searches API for seriesID
            seriesId = await getter.SearchForSeries(seriesName);
            if (seriesId == -1)
            {
                result.result = false;
                result.errorMessage = "Series not found";
                return result;
            }

            // Gets series
            series = await getter.GetSeries(seriesId);

            if (series.seriesName != null)
            {
                // Adds series to database
                List<Episode> episodes = await AddSeason(series.id, database);
                series.episodes = episodes;

                database.Add(series);
                foreach (Episode episode in episodes)
                {
                    database.Add(episode);
                }
                database.SaveChanges();
                result.result = true;

            }
            else
            {
                // Creates error message
                result.result = false;
                result.errorMessage = "Series id not found";
            }

            return result;
        }
        public async Task<List<Episode>> AddSeason(int seriesId, Database database)
        {
            List<Episode> episodes;
            ResultContainer result = new ResultContainer();

            GetImdbSeries getter = new GetImdbSeries();
            episodes = await getter.getEpisodeList(seriesId);

            if (episodes.Count == 0)
            {
                result.result = false;
                result.errorMessage = "No episodes found [Database.cs]";
                //return result;
            }

            //result.result = true;
            return episodes;
        }
    }
}
