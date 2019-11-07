using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using MediaCalender.Shared.Containers;
using MediaCalender.Shared.ContentTypes;

namespace MediaCalender.Server.CsClasses
{
    public class DatabaseOLD
    {
        SQLiteConnection sql_connection { get; }
        GetImdbMedia imdbAPI { get; }

        public DatabaseOLD()
        {
            // Make connection to database
            sql_connection = new SQLiteConnection("Data Source=Database.sqlite;Version=3;");
            sql_connection.Open();
            imdbAPI = new GetImdbMedia();
        }

        private SQLiteDataReader GetSqlReader(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, sql_connection);
            SQLiteDataReader reader = command.ExecuteReader();
            return reader;
        }

        #region LoginSystem
        public bool CheckLoginCredentials(LoginCredentials credentials)
        {
            string sql = $"SELECT * FROM User WHERE Username = '{credentials.username}'" +
                $" AND Password = '{credentials.password}'";
            SQLiteDataReader reader = GetSqlReader(sql);

            reader.Read();
            if (reader.HasRows)
                return true;
            else
                return false;
        }
        #endregion LoginSystem

        Movie retrieveSpecificMovie(int key)
        {
            Movie movie = new Movie();

            SQLiteDataReader reader = GetSqlReader($"SELECT * from MovieInfo WHERE Key = {key}");

            reader.Read();

            movie.Title = (string)reader["Title"];
            movie.Year = Convert.ToInt32(reader["Year"]);
            movie.Rated = (string)reader["Rated"];
            movie.Released = (string)reader["Released"];
            movie.Runtime = (string)reader["Runtime"];
            movie.Genre = (string)reader["Genre"];
            movie.Director = (string)reader["Director"];
            movie.Writer = (string)reader["Writer"];
            movie.Actors = (string)reader["Actors"];
            movie.Plot = (string)reader["Plot"];
            movie.Language = (string)reader["Language"];
            movie.Country = (string)reader["Country"];
            movie.Awards = (string)reader["Awards"];
            movie.Poster = (string)reader["Poster"];
            movie.Metascore = Convert.ToInt32(reader["Year"]);
            movie.imdbRating = Convert.ToDouble(reader["ImdbRating"]);
            movie.imdbVotes = (string)reader["ImdbVotes"];
            movie.imdbID = (string)reader["ImdbID"];
            movie.Type = (string)reader["Type"];

            return movie;
        }

        public string PrintSpecificMovie(int key)
        {
            Movie movie = retrieveSpecificMovie(key);

            return movie.ToString();
        }

        public async Task<BoolContainer> AddMovieAsync(string movieName)
        {
            Movie movie;
            bool result;

            //movie = imdbAPI.getMovie(movieName);
            movie = new Movie();
            GetImdbSeries getter = new GetImdbSeries();
            //Task<Episode> TEpisode = await getter.getSeries("hd");
            //Episode episode = await getter.getEpisode("hd");
            var episode = await getter.getEpisodeList(2);
            ////Episode episode = TEpisode.Result;
            //AddEpisodeToDatabase(episode);            // temp
            //getter.SetToken();

            if (movie.Response == "True")
            {
                AddMovieToDatabase(movie);
                result = true; 
            }
            else
                result = false;

            return new BoolContainer() { result = result };
        }

        public async Task<ResultContainer> AddSeason(Series series)
        {
            List<Episode> episodes;
            ResultContainer result = new ResultContainer();

            GetImdbSeries getter = new GetImdbSeries();
            episodes = await getter.getEpisodeList(2);

            if (episodes.Count == 0)
            {
                result.result = false;
                result.errorMessage = "No episodes found [Database.cs]";
                return result;
            }

            foreach (Episode episode in episodes)
            {
                AddEpisodeToDatabase(episode);
            }

            result.result = true;
            return result;
        }

        public async Task<ResultContainer> AddFolSeries(string seriesName)
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

            // Stops if series already is in database
            //if (CheckIfInDB(seriesId) == true)
                


            // Gets info about series
            series = await getter.GetSeries(seriesId);

            if (series.seriesName != null)
            {
                // Adds series to database
                AddSeriesToDatabase(series);
                // Adds the season to the database
                await AddSeason(series);
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

        void AddMovieToDatabase(Movie movie)
        {
            string sql = $"Insert into movieInfo (Title, Year, Rated, Released, Runtime, Genre, Director, Writer, Actors, Plot, Language, Country, Awards, Poster, Metascore, ImdbRating, ImdbVotes, ImdbID, Type)" +
                $" values ('{movie.Title.Replace("'", "´")}', '{movie.Year}', '{movie.Rated.ToString()}'" +
                $", '{movie.Released}', '{movie.Runtime}', '{movie.Genre}'" +
                $", '{movie.Director}', '{movie.Writer}', '{movie.Actors.Replace("'", "´")}'" +
                $", '{movie.Plot.Replace("'", "´")}', '{movie.Language}', '{movie.Country}'" +
                $", '{movie.Awards}', '{movie.Poster}', '{movie.Metascore}'" +
                $", '{movie.imdbRating.ToString().Replace(",", ".")}', '{movie.imdbVotes}'" +
                $", '{movie.imdbID}', '{movie.Type}')";

            SQLiteDataReader reader = GetSqlReader(sql);
        }

        public void AddSeriesToDatabase(Series series)
        {
            string sql = $"Insert into seriesLibary (Id, SeriesName, Aliases, Banner, SeriesId, Status, FirstAired, Network, NetworkId, Runtime, Genre, Overview, LastUpdated, AirsDayOfWeek, AirsTime, Rating, ImdbId, Zap2itId, Added, SiteRating, SiteRatingCount, Slug)" +
                //$" values ('{series.id}', '{series.seriesName.Replace("'", "´")}', '{series.aliases.Replace("'", "´")}'" +
                //$", '{series.banner.Replace("'", "´")}', '{series.seriesId.Replace("'", "´")}'" +
                //$", '{series.status.Replace("'", "´")}', '{series.firstAired.Replace("'", "´")}'" +
                //$", '{series.firstAired.Replace("'", "´")}', '{series.network.Replace("'", "´")}'" +
                //$", '{series.runtime.Replace("'", "´")}', '{series.genre.Replace("'", "´")}'" +
                //$", '{series.overview.Replace("'", "´")}', '{series.lastUpdated}'" +
                //$", '{series.airsDayOfWeek.Replace("'", "´")}', '{series.airsTime.Replace("'", "´")}'" +
                //$", '{series.rating.Replace("'", "´")}', '{series.imdbId.Replace("'", "´")}'" +
                //$", '{series.zap2itId.Replace("'", "´")}', '{series.added.Replace("'", "´")}'" +
                $", '{series.siteRating}', '{series.siteRatingCount}', '{series.slug}')";

            SQLiteDataReader reader = GetSqlReader(sql);
        }

        public void AddEpisodeToDatabase(Episode episode)
        {
            string sql = $"Insert into episodeLibary (Id, AiredSeason, AiredSeasonId, AiredEpisodeNumber, EpisodeName, FirstAired, GuestStars, Director, Directors, Writers, Overview, ProductionCode, ShowUrl, LastUpdated, AbsoluteNumber, Filename, SeriesId, LastUpdatedBy, ThumbAdded, ThumbWidth, ThumbHeight, ImdbId, SiteRating, SiteRatingCount)" +
                $" values ('{episode.id}', '{episode.airedSeason}', '{episode.airedSeasonID}'" +
                $", '{episode.airedEpisodeNumber}', '{episode.episodeName.Replace("'", "´")}'" +
                $", '{episode.firstAired.Replace("'", "´")}', '{episode.guestStars.Replace("'", "´")}'" +
                $", '{episode.director.Replace("'", "´")}', '{episode.directors.Replace("'", "´")}'" +
                $", '{episode.writers.Replace("'", "´")}', '{episode.overview.Replace("'", "´")}'" +
                $", '{episode.productionCode.Replace("'", "´")}', '{episode.showUrl.Replace("'", "´")}'" +
                $", '{episode.lastUpdated}', '{episode.absoluteNumber}', '{episode.filename.Replace("'", "´")}'" +
                $", '{episode.seriesId}', '{episode.lastUpdatedBy}', '{episode.thumbAdded.Replace("'", "´")}'" +
                $", '{episode.thumbWidth.Replace("'", "´")}', '{episode.thumbHeight.Replace("'", "´")}'" +
                $", '{episode.imdbId.Replace("'", "´")}', '{episode.siteRating}', '{episode.siteRatingCount}')";

            SQLiteDataReader reader = GetSqlReader(sql);
        }

        public async Task<List<Episode>> GetAllEpisodes()
        {
            List<Episode> episodeList = new List<Episode>();

            string sql = "SELECT * FROM EpisodeLibary";
            SQLiteDataReader reader = GetSqlReader(sql);

            // MANY attributes are missing, also inbetween the already added ! ! !
            while (reader.Read())
            {
                Episode episode = new Episode();
                episode.id = Convert.ToInt32(reader["Id"]);
                episode.airedSeason = Convert.ToInt32(reader["AiredSeason"]);
                episode.airedEpisodeNumber = Convert.ToInt32(reader["AiredEpisodeNumber"]);
                episode.episodeName = reader["EpisodeName"].ToString();
                episode.firstAired = reader["FirstAired"].ToString();
                episode.overview = reader["Overview"].ToString();

                episodeList.Add(episode);
            }

            return episodeList;
        }

        private bool CheckIfInDB(int Id)
        {
            bool isInDB = false;

            string sql = "SELECT * FROM SeriesLibary";
            SQLiteDataReader reader = GetSqlReader(sql);

            // MANY attributes are missing, also inbetween the already added ! ! !
            while (reader.Read())
            {
                int checkedID;
                checkedID = Convert.ToInt32(reader["Id"]);

                if (Id == checkedID)
                    return true;
            }

            return isInDB;
        }
    }
}



//public string PrintString()
//{
//    string sql = "select * from User";
//    SQLiteDataReader reader = GetSqlReader(sql);

//    string result = null;
//    while (reader.Read())
//    {
//        result += reader["Key"].ToString()
//        + reader["Username"].ToString()
//        + "[]"
//        + reader["Password"].ToString();
//    }

//    return result;
//}