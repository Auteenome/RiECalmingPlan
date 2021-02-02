using MvvmHelpers.Commands;
using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    class ViewModel_Login : ViewModel_Base {
        // View Model for login procedure
        // Has properties for Email, Password
        // Has command LoginCommand that checks entries not null or empty, and calls User.CheckLoginInformation(Email, Password)
        // Has Sign Up command

        private string email;
        private string password;

        public ViewModel_Login()
        {

        }

        public string Email {
            get { return email; }
            set {
                SetProperty(ref email, value);
            }
        }

        public string Password {
            get { return password; }
            set {
                SetProperty(ref password, value);
            }
        }

        public Command LoginCommand {
            get { return new Command(Login); }
        }
        private void Login()
        {
            // for now this only validates with dummy data email@email.com + password
            // in future should query web DB

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                App.Current.MainPage.DisplayAlert("Login", "Login unuccessful, empty username or password.", "Okay");
            else
            {
                if (User.CheckLoginInformation(Email, Password))
                {
                    App.Current.MainPage.DisplayAlert("Login", "Login successful", "Okay");
                    App.Current.MainPage.Navigation.PushAsync(new Pages.Page_Menu());
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
            // redirect to Terms and Conditions if they have not been accepted
            if (AppPreferences.TermsAndConditionsBottomControls)
                App.Current.MainPage.Navigation.PushAsync(new Pages.Page_Register());
            else
                App.Current.MainPage.Navigation.PushAsync(new Pages.Page_TermsAndConditions());
        }
        
    }
}
