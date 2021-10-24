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
                string query = "SELECT * FROM wellbeing_schema.Users";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UserDto user = new UserDto();
                    user.ID = reader.GetString(0);
                    user.Name = reader.GetString(1);
                    user.PHONENumber = reader.GetString(2);
                    user.BDayConsent = reader.GetString(4);
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
                string query = $"INSERT INTO `wellbeing_schema`.`Users` (`id`, `Name`, `PhoneNumber`, `BirthDay`, `BDayConsent`) VALUES('{user.ID}', '{user.Name}', '{user.PHONENumber}', '{user.BirthDay}', '{user.BDayConsent}')";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.ExecuteNonQuery();

                query = $"INSERT INTO `wellbeing_schema`.`steps` (`id`, `CountPerDay`) VALUES('{user.ID}', '0')";
                cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.ExecuteNonQuery();
                
                dbCon.Close();
            }
        }

        public void UpdateUserSteps(int id, int steps)
        {
            DBConnection dbCon = DBConnection.Instance();

            if (dbCon.IsConnect())
            {
                //suppose col0 and col1 are defined as VARCHAR in the DB

                string query = $"UPDATE `wellbeing_schema`.`steps` SET `CountPerDay` = '{steps}' WHERE (`id` = '{id}')";
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
                string query = "SELECT * FROM wellbeing_schema.steps";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UserStepsDto steps = new UserStepsDto();
                    steps.ID = reader.GetInt32(0);
                    steps.steps = reader.GetString(1);

                    userSteps.Add(steps);
                }

                dbCon.Close();
            }
            return userSteps;
        }

        public UserStepsDto GetUserSteps(int id)
        {
            return GetUserSteps().Where(x => x.ID == id).FirstOrDefault();
        }

    }
}
