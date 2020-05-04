using System;
using Xamarin.Forms;
using RiECalmingPlan.Views;
using Xamarin.Forms.Xaml;
using RiECalmingPlan.Models;


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
            Init();
        }

        void Init() {
            BackgroundColor = Constants.BackgroundColor;
            Lbl_Username.TextColor = Constants.MainTextColor;
            Lbl_Password.TextColor = Constants.MainTextColor;
            //Do not display the activity spinner
            ActivitySpinner.IsVisible = false;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;

            //When the entry Username is completed and return key pressed, the focus moves to the password entry
            //Entry_UserName.Completed += (s, e) => Entry_PassWord.Focus();
            //Entry_PassWord.Completed += (s, e) => SignInProcedure(s, e);
        }



        private async void Page2Jump(object sender, EventArgs e) {
            await Navigation.PushAsync(new Page_Menu());
        }

        void SignInProcedure(object sender, EventArgs e) {
            /* Use later when connected to Web
             
             User user = new User(Entry_UserName.Text, Entry_PassWord.Text);
            if (user.CheckLoginInformation())
            {
                //DisplayAlert("Login", "Login Successful", "Okay");
                //App.UserDatabase.SaveUser(user);
            }
            else
            {
                DisplayAlert("Login", "Login Not Successful, empty username or password.", "Okay");
            }
            */
        }

       

        void GoToTandC(object sender, EventArgs e) {

        }
    
    }
}
