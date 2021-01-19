using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    class ViewModel_Register : ViewModel_Base {
        private string email;
        private string password;

        public ViewModel_Register()
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

        public Command RegisterCommand {
            get { return new Command(Register); }
        }
        private void Register()
        {
            ViewModels.AppPreferences.AccountCreated = true;
            App.Current.MainPage.Navigation.PushAsync(new Pages.Page_Menu());
        }
    }
}
