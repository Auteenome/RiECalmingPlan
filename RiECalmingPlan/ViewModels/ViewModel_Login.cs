using MvvmHelpers.Commands;
using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    class ViewModel_Login : INotifyPropertyChanged {
        // View Model for login procedure
        // Has properties for Email, Password
        // Has command LoginCommand that checks entries not null or empty, and calls User.CheckLoginInformation(Email, Password)
        // Last Updated by: Mitchell H 13.07.20

        public event PropertyChangedEventHandler PropertyChanged;
        private string email;
        private string password;

        public ViewModel_Login()
        {

        }

        public string Email {
            get { return email; }
            set {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        public string Password {
            get { return password; }
            set {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Passowrd"));
            }
        }

        public Command LoginCommand {
            get { return new Command(Login); }
        }
        private void Login()
        {
            // for now this only validates with dummy data email@email.com + password
            // in future should query web DB

            // null or empty validation
            // check entries not null
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                App.Current.MainPage.DisplayAlert("Login", "Login unuccessful, empty username or password.", "Okay");
            else
            {
                if (User.CheckLoginInformation(Email, Password))
                {
                    App.Current.MainPage.DisplayAlert("Login", "Login successful", "Okay");
                    App.Current.MainPage.Navigation.PushAsync(new Views.Page_Menu());
                } 
                else
                    App.Current.MainPage.DisplayAlert("Login", "Login unsuccessful, username or password incorrect", "Okay");
            }
        }

        public Command SignUpCommand {
            get { return new Command(SignUp); }
        }
        private void SignUp()
        {
            App.Current.MainPage.DisplayAlert("Sign Up", "Sign Up functionality not yet implemented", "Okay");
        }
        
    }
}
