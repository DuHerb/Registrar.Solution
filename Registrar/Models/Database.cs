using System;
using MySql.Data.MySqlClient;
using Registrar;

namespace Registrar.Models
{

    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }

        public static void Close(MySqlConnection conn)
        {
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
    }

    public static class DBConfiguration
    {
      public static string ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=registrar;";
    }
}
