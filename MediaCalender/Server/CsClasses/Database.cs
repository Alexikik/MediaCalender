using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using MediaCalender.Shared.Containers;
using MediaCalender.Shared.ContentTypes;

namespace MediaCalender.Server.CsClasses
{
    public class Database
    {
        SQLiteConnection sql_connection { get; }
        GetImdbMedia imdbAPI { get; }

        public Database()
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

        public BoolContainer AddMovie(string movieName)
        {
            Movie movie;
            bool result;

            //movie = imdbAPI.getMovie(movieName);
            movie = new Movie();
            GetImdbSeries getter = new GetImdbSeries();
            getter.getSeries("hd");
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
    }
}
