using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCalender.Shared.ContentTypes;
using MediaCalender.Shared.Containers;
using System.Net;
//using System.Web.Script.Serialization;
using Nancy.Json;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace MediaCalender.Server.CsClasses
{
    public class GetImdbMedia
    {
        public Movie getMovie(string txtMovieName)
        {
            MovieRar obj = new MovieRar();
            string url = $"http://www.omdbapi.com/?t={txtMovieName}&apikey=24cd4afb";             // Gets a movie/series
            //string url = $"http://www.omdbapi.com/?t={txtMovieName}&season=1&apikey=24cd4afb";    // Gets a specific season
            //string url = $"http://www.omdbapi.com/?t={txtMovieName}&season=1&episode=8&apikey=24cd4afb";      // Gets a specific episode from a specific season
            //string url = $"http://www.omdbapi.com/?t={txtMovieName}&season=23&apikey=24cd4afb";      // South Park Season 23
            //string url = $"http://www.omdbapi.com/?t={txtMovieName}&season=23&episode=4&apikey=24cd4afb";   // South Park S23E4
            //string url = $"http://www.omdbapi.com/?t={txtMovieName}&season=18&episode=2&apikey=24cd4afb";      // Family Guy Season 18
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

    public class GetImdbSeries
    {
        Token Token { get; }
        readonly string ApiKey = "43UZATVN8H64OAW8";

        public GetImdbSeries()
        {
            Task<Token> token = SetToken();
            this.Token = token.Result;
        }

        //Sets the token from the apikey from tvdb
        public async Task<Token> SetToken()
        {
            using (HttpClient client = new HttpClient())
            {
                //Url to TVDB
                Uri url = new Uri("https://api.thetvdb.com/login");

                //Set Accept request header
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                //Setup request message with json apikey and content-type header
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent("{\"apiKey\":\"" + ApiKey + "\"}", Encoding.UTF8, "application/json");

                //Send via post, get response, read content into string, check to be sure it was OK
                HttpResponseMessage resp = await client.SendAsync(request);
                string respString = await resp.Content.ReadAsStringAsync();
                if (resp.ReasonPhrase != "OK")
                {
                    throw new Exception(resp.ReasonPhrase);
                }

                // Deserialize string into token
                JavaScriptSerializer oJS = new JavaScriptSerializer();
                return oJS.Deserialize<Token>(respString);
            }
        }

        // Gets a series from the API
        public async Task getSeries(string txtSeriesName)
        {
            using (HttpClient client = new HttpClient())
            {
                //Url to TVDB
                //Uri url = new Uri("https://api.thetvdb.com/search/series?name=South%20Park");
                //Uri url = new Uri("https://api.thetvdb.com/episodes/75897");
                Uri url = new Uri("https://api.thetvdb.com/series/75897/episodes/query?airedSeason=23&airedEpisode=5");

                //Set Accept request header
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.token);

                //Setup request message with json apikey and content-type header
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                //request.Content = new StringContent("Authorization: Bearer " + Token.token, Encoding.UTF8, "application/json");
                //request.Headers.Add("Authorization", $"{Token.token}");

                //Send via post, get response, read content into string, check to be sure it was OK
                HttpResponseMessage resp = await client.SendAsync(request);
                string respString = await resp.Content.ReadAsStringAsync();
                if (resp.ReasonPhrase != "OK")
                {
                    throw new Exception(resp.ReasonPhrase);
                }

                // Deserialize string into token
                JavaScriptSerializer oJS = new JavaScriptSerializer();
                Token t = oJS.Deserialize<Token>(respString);
            }
        }
    }
}
