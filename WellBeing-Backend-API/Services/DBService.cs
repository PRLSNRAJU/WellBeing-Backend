using Common.Dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellBeingBackendAPI.Services
{
    public class DBService
    {
        public DBService()
        {

        }

        public List<UserDto> Users { get => GetUsers(); }

        //QUERIES
        private List<UserDto> GetUsers()
        {
            DBConnection dbCon = DBConnection.Instance();
            List<UserDto> Users = new List<UserDto>();
            if (dbCon.IsConnect())
            {
                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "SELECT * FROM wellbeing_schema.users";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UserDto user = new UserDto();
                    user.PHONENumber = reader.GetString(0);
                    user.Name = reader.GetString(1);
                    user.BDayConsent = reader.GetString(2);
                    user.BirthDay = user.BDayConsent == "1" ? reader.GetString(3) : string.Empty;

                    Users.Add(user);
                }

                dbCon.Close();
            }
            return Users;
        }

        public void AddUser(UserDto user)
        {
            DBConnection dbCon = DBConnection.Instance();
            
            if (dbCon.IsConnect())
            {
                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = $"INSERT INTO `wellbeing_schema`.`users` (`PhoneNumber`, `Name`, `BDayConsent`, `BirthDay`) VALUES('{user.PHONENumber}', '{user.Name}', '{user.BDayConsent}', '{user.BirthDay}')";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.ExecuteNonQuery();

                query = $"INSERT INTO `wellbeing_schema`.`steps` (`PhoneNumber`, `CountPerDay`) VALUES('{user.PHONENumber}', '0')";
                cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.ExecuteNonQuery();
                
                dbCon.Close();
            }
        }

        public void UpdateUserSteps(string phoneNumber, int steps)
        {
            DBConnection dbCon = DBConnection.Instance();

            if (dbCon.IsConnect())
            {
                //suppose col0 and col1 are defined as VARCHAR in the DB

                string query = $"UPDATE `wellbeing_schema`.`steps` SET `CountPerDay` = '{steps}' WHERE (`PhoneNumber` = '{phoneNumber}')";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.ExecuteNonQuery();

                dbCon.Close();
            }
        }

        public List<UserStepsDto> GetUserSteps()
        {
            DBConnection dbCon = DBConnection.Instance();
            List<UserStepsDto> userSteps = new List<UserStepsDto>();
            if (dbCon.IsConnect())
            {
                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "SELECT * FROM wellbeing_schema.steps order by CountPerDay desc";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UserStepsDto steps = new UserStepsDto();
                    steps.PhoneNumber = reader.GetString(0);
                    steps.steps = Convert.ToString(reader.GetInt32(1));

                    userSteps.Add(steps);
                }

                dbCon.Close();
            }
            return userSteps;
        }

        public UserStepsDto GetUserSteps(string id)
        {
            return GetUserSteps().Where(x => x.PhoneNumber == id).FirstOrDefault();
        }

    }
}
