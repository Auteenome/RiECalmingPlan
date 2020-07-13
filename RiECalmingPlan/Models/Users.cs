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
            // Basic check only to see entered data matches some dummy data
            // Later this will query the web database

            if (this.Username.Equals("email@email.com") && this.Password.Equals("password"))
                return true;
            else
                return false;
        }

        public static bool CheckLoginInformation(string Email, string Password)
        {
            // method for MVVM login procedure

            if (Email.Equals("email@email.com") && Password.Equals("password"))
                return true;
            else
                return false;
        }

    }
}
