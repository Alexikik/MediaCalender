using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediaCalender.Shared.Containers;
using MediaCalender.Shared.ContentTypes;
using MediaCalender.Server.CsClasses;

namespace MediaCalender.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {
        Database database;
        SeriesLibary seriesLibary;
        MovieLibary movieLibary;
        public CalendarController(Database database, SeriesLibary seriesLibary, MovieLibary movieLibary)
        {
            this.database = database;
            this.seriesLibary = seriesLibary;
            this.movieLibary = movieLibary;
        }

        [HttpPost("[action]")]
        public async Task<ResultContainer> AddFolSeries([FromBody]StringContainer stringContainer)
        {
            ResultContainer result;
            result = await seriesLibary.AddSeries(stringContainer.str, database);

            return result;
        }

        // Adds a specific movie by name and returns a bool indicating if the process was succesful
        [HttpPost("[action]")]
        public ResultContainer AddMovie([FromBody]StringContainer stringContainer)
        {
            ResultContainer result;

            result = movieLibary.AddMovie(stringContainer.str);

            return result;
        }

        // Returns all episodes from the db
        [HttpPost("[action]")]
        public List<Episode> GetAllEpisodes()
        {
            List<Episode> episodeList = new List<Episode>();
            episodeList = database.EpisodeLibary.Where(e => e.firstAired > DateTime.Now.AddMonths(-1)).ToList();

            //episodeList = database.EpisodeLibary.Where(s => s.Key < 25).ToList();
            //episodeList = database.EpisodeLibary.Where(s => s.Key < 25).ToList();
            //episodeList.Add(database.EpisodeLibary.Where(s => s.Key == 167).First());

            foreach (Episode episode in episodeList)
            {
                if (episode.firstAired == null)
                {
                    episode.firstAired = DateTime.Now;
                }
            }

            //foreach (Series series in database.SeriesLibary)
            //{
            //    foreach (Episode episode in series.episodes)
            //    {
            //        episodeList.Add(episode);
            //    }
            //}


            return episodeList;
        }

        [HttpPost("[action]")]
        public void ClearDatabase()
        {
            foreach (Episode episode in database.EpisodeLibary)
            {
                database.Remove(episode);
            }
            foreach (Series series in database.SeriesLibary)
            {
                database.Remove(series);
            }
            database.SaveChanges();
        }














        // Gets a string with some info on a specific movie (TEST)
        [HttpGet("[action]")]
        public StringContainer GetSpecificMovieInfo()
        {
            StringContainer answer = new StringContainer() { str = Program.Classes.database.PrintSpecificMovie(1) };
            return answer;
        }


        
        
        

        
    }
}



//[HttpPost("[action]")]
//public async Task<ResultContainer> AddFolSeries([FromBody]StringContainer stringContainer)
//{
//    ResultContainer result;
//    result = await Program.Classes.database.AddFolSeries(stringContainer.str);

//    return result;
//}