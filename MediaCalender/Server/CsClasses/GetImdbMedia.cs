using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCalender.Shared.ContentTypes;
using MediaCalender.Shared.Containers;
using System.Net;
//using System.Web.Script.Serialization;
using Nancy.Json;

namespace MediaCalender.Server.CsClasses
{
    public class GetImdbMedia
    {
        public Movie getMovie(string txtMovieName)
        {
            MovieRar obj = new MovieRar();
            //string url = $"http://www.omdbapi.com/?t={txtMovieName}&apikey=24cd4afb";             // Gets a movie/series
            //string url = $"http://www.omdbapi.com/?t={txtMovieName}&season=1&apikey=24cd4afb";    // Gets a specific season
            //string url = $"http://www.omdbapi.com/?t={txtMovieName}&season=1&episode=8&apikey=24cd4afb";      // Gets a specific episode from a specific season
            //string url = $"http://www.omdbapi.com/?t={txtMovieName}&season=23&apikey=24cd4afb";      // South Park Season 23
            //string url = $"http://www.omdbapi.com/?t={txtMovieName}&season=23&episode=4&apikey=24cd4afb";   // South Park S23E4
            string url = $"http://www.omdbapi.com/?t={txtMovieName}&season=18&episode=2&apikey=24cd4afb";      // Family Guy Season 18
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(url);
                //json.Replace("'", "´"); // Replaces "'" with "´"
                JavaScriptSerializer oJS = new JavaScriptSerializer();
                Console.WriteLine(json);
                obj = oJS.Deserialize<MovieRar>(json);
                //if (obj.Response == "True")
                //    Console.WriteLine($"Succes!");
                //else
                //    Console.WriteLine("Error");
            }
            
            return convertRarToMovie(obj);
        }

        private Movie convertRarToMovie(MovieRar movieRar)
        {
            Movie movie = new Movie();
            int intTemp;
            bool result;
            double doubleTemp;

            movie.Title = movieRar.Title;

            result = Int32.TryParse(movieRar.Year, out intTemp);
            if (result)
                movie.Year = intTemp;
            else
                movie.Year = 0;

            movie.Rated = movieRar.Rated;
            movie.Released = movieRar.Released;
            movie.Runtime = movieRar.Runtime;
            movie.Genre = movieRar.Genre;
            movie.Director = movieRar.Director;
            movie.Writer = movieRar.Writer;
            movie.Actors = movieRar.Actors;
            movie.Plot = movieRar.Plot;
            movie.Language = movieRar.Language;
            movie.Country = movieRar.Country;
            movie.Awards = movieRar.Awards;
            movie.Poster = movieRar.Poster;

            result = Int32.TryParse(movieRar.Metascore, out intTemp);
            if (result)
                movie.Metascore = intTemp;
            else
                movie.Metascore = 0;

            result = double.TryParse(movieRar.imdbRating, out doubleTemp);
            if (result)
                movie.imdbRating = doubleTemp;
            else
                movie.imdbRating = 0;

            movie.imdbVotes = movieRar.imdbVotes;
            movie.imdbID = movieRar.imdbID;
            movie.Type = movieRar.Type;
            movie.Response = movieRar.Response;

            return movie;
        }
    }
}
