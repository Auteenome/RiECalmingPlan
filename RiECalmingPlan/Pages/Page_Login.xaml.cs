using System;
using Xamarin.Forms;
using RiECalmingPlan.Views;
using Xamarin.Forms.Xaml;
using RiECalmingPlan.Models;
using RiECalmingPlan.Pages;
using Xamarin.Forms.Internals;
using RiECalmingPlan.ViewModels;

namespace RiECalmingPlan.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //This sets the wait compile

    public partial class Page_Login : ContentPage {
        public Page_Login() {
            //This is not used until a Web connection to the RiE System
            //  is required.
            //Assume for now that there is just one user (the phone user) with a password
            //  That can be retrieved only from a web source if it is lost.
            //So just get acceptance of Terms and Conditions plus
            //  user registration details (email as username, emergency contact, etc
            //  and a password for this draft system.
            InitializeComponent();
            BindingContext = new ViewModel_Login();
            Init();
        }

        void Init() {
            // BackgroundColor = Constants.BackgroundColor;
            //Lbl_Username.TextColor = Constants.MainTextColor;   // hides labels by making them white
            //Lbl_Password.TextColor = Constants.MainTextColor;

            //Do not display the activity spinner
            //ActivitySpinner.IsVisible = false;
            //LoginIcon not currently used
            //LoginIcon.HeightRequest = Constants.LoginIconHeight;

            //When the entry Username is completed and return key pressed, the focus moves to the password entry
            // Entry_Email.Completed += (s, e) => Entry_Password.Focus();
            // Entry_Password.Completed += (s, e) => SignInProcedure(s, e);
        }



        private async void Page2Jump(object sender, EventArgs e) {
            await Navigation.PushAsync(new Page_Menu());
        }

        /*
         * functionality moved to ViewModelLogin 
        private async void SignInProcedure(object sender, EventArgs e) {
            // To be added: web functionality (for now only checks entries are not null, and data matches dummy data 

            // check entries not null
            if (string.IsNullOrEmpty(Entry_Email.Text) || string.IsNullOrEmpty(Entry_Password.Text))
                await DisplayAlert("Login", "Login unuccessful, empty username or password.", "Okay");
            else 
            {
                User user = new User(Entry_Email.Text, Entry_Password.Text);

                // check login credentials
                if (user.CheckLoginInformation())
                {
                    await DisplayAlert("Login", "Login succseful", "Okay");
                    await Navigation.PushAsync(new Page_Menu());
                } 
                else
                    await DisplayAlert("Login", "Login unsuccessful, username or password incorrect", "Okay");
            }
        }
        */

        /*
         * functionality moved to ViewModelLogin 
        void GoToTandC(object sender, EventArgs e) {

        }
        */

        private async void GoToResetPassword(object sender, EventArgs e)
        {
            await DisplayAlert("Reset Password", "This functionality has not been added yet", "Okay");
        }
    }
}
