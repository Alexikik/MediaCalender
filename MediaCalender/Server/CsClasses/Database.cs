using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using MediaCalender.Shared.Containers;

namespace MediaCalender.Server.CsClasses
{
    public class Database
    {
        public SQLiteConnection sql_connection { get; }

        public Database()
        {
            // Make connection to database
            sql_connection = new SQLiteConnection("Data Source=Database.sqlite;Version=3;");
            sql_connection.Open();
        }

        private SQLiteDataReader GetSqlReader(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, sql_connection);
            SQLiteDataReader reader = command.ExecuteReader();
            return reader;
        }

        public string PrintString()
        {
            string sql = "select * from User";
            SQLiteDataReader reader = GetSqlReader(sql);

            string result = null;
            while (reader.Read())
            {
                result += reader["Key"].ToString()
                + reader["Username"].ToString()
                + "[]"
                + reader["Password"].ToString();
            }

            return result;
        }

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
    }
}
