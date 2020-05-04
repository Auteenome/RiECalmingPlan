using System;
using SQLite;

namespace RiECalmingPlan.Models
{
    public class User
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Valid { get; set; }
        public bool Accepted { get; set; }


        public User()
        {
        }

        public User(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
            Valid = true; //This means the use is a valid user - to be saved to the DB
            Accepted = true; //this means the user has accepted the terms and conditions initially - to be saved to the DB

        }

        public bool CheckLoginInformation()
        {
            //Basic check only to see some text was entered in the Login Page Entry form , ie that username is not blank
            if (!this.Username.Equals("") && !this.Password.Equals(""))
                return true;
            else
                return false;
        }

    }
}
