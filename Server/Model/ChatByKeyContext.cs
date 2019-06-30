using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace Server.Model
{
    public class ChatByKeyContext
    {
        public string ConnectionString { get; set; }

        public ChatByKeyContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public User GetUtenteByUsernameAndPassword(string username, string password)
        {

            try
            {

                using (IDbConnection db = GetConnection())
                {
                    string sql = "Select * From utente WHERE username = @username and password = @password";
                    var utenti = db.Query<User>(sql, new { username, password }).ToList();

                    return utenti.FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                //loggare l'errore
            }

            return null;

        }

        //public User InsertUser(User user)
        //{

        //    try
        //    {

        //        using (IDbConnection db = GetConnection())
        //        {
        //            string sql = "Insert into";
        //            var utenti = db.Query<User>(sql, new { username, password }).ToList();

        //            return utenti.FirstOrDefault();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //loggare l'errore
        //    }

        //    return null;

        //}
    }
}

