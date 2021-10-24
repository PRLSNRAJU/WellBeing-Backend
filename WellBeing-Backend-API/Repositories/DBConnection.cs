using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellBeingBackendAPI.Services
{
    public class DBConnection
    {
        private DBConnection()
        {

        }

        private readonly string server = "wellbeing.cu52mgn5usow.ap-south-1.rds.amazonaws.com";
        private readonly string dbName = "wellbeing_schema";
        private readonly string userName = "admin";
        private readonly string password = "WellBeing";

        public MySqlConnection Connection { get; set; }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(dbName))
                    return false;
                string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", server, dbName, userName, password);
                Connection = new MySqlConnection(connstring);
            }

            Connection.Open();

            return true;
        }

        public void Close()
        {
            Connection.Close();
        }
    }
}
