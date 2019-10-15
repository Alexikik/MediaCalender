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
        public Series getSeries(string txtSeriesName)
        {
            Series series;
            string url = $"https://api.thetvdb.com/search/series?name=South%20Park";
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Add("Authorization:-Bearer", "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1NzExNTcwNjgsImlkIjoiTWVkQ2FsIiwib3JpZ19pYXQiOjE1NzEwNzA2NjgsInVzZXJpZCI6NTQwNjMyLCJ1c2VybmFtZSI6IkFsZXhpazE5OThtaXUifQ.D240p8NzQL7xMiaHyAeF6PWeKVJ_IG987-r0amNuE1ix4NTPEHAsmvcnzRGyhsE6CMPJtU4Xi6Bs0QiX4mpqS5txEQCV7ZmIngV4hCpwUtxt2XOe7noBlPfSqkTHcvTGS5QlUqwn0GpZA1LxadkzpfHpbcrREZf-aJ472gXE_1iaOAlmUJZClVri9B_eTSPFeOYR5OtevW3BSXx0dSO5GfRDjtWTTpyrcfbFKYIYziKDK5kWtgt2gvuSgK2M2t58zfMZ_WwfNyDnoXm9gPxzve1M5hTSsUYHrjGpE7tUGLPCSNEFHMP_Z7yudpT7tPtoPjxhR9Ij6XowWIgI9RTn_A");
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                
                var json = webClient.DownloadString(url);
                //json.Replace("'", "´"); // Replaces "'" with "´"
                JavaScriptSerializer oJS = new JavaScriptSerializer();
                Console.WriteLine(json);
                series = oJS.Deserialize<Series>(json);
            }

            return new Series();
        }

        //Sets the token from the apikey from tvdb
        private async Task SetToken(string ApiKey)
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

                //Read into byte array and them create a memory stream
                byte[] byteArray = Encoding.UTF8.GetBytes(respString);
                MemoryStream stream1 = new MemoryStream(byteArray);

                //Create serializer and read the stream into a Token object
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(string));
                _token = serializer.ReadObject(stream1) as Token;

                //Set global token for debugging
                tokenString = _token.token;
            }
        }

    }
}
