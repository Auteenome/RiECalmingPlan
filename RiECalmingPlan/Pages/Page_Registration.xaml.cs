using Xamarin.Forms;
using RiECalmingPlan.Models;

namespace RiECalmingPlan.Views {
    public partial class Page_Registration : ContentPage {

        public Page_Registration() {
            InitializeComponent();
            BackgroundColor = Constants.BackgroundColor;
        }

        async void RegisterNewUser(object sender, System.EventArgs e) {
            Application.Current.Properties["userLogin"] = Entry_UserName.Text;
            Application.Current.Properties["usePassword"] = Entry_PassWord.Text;

            // Check to see a valid email for username
            if (Entry_UserName != null) {
                //Application.Current.Properties["loginOK"] = "OK";
                Application.Current.Properties["isRegistered"] = "Registered";
            

            //To get here, Terms and conditions must have been agreed  to
            //If there is a valid email as username then go to the main app again.
            //Don't wait for sleep or quit - save the Application System properties
            await Application.Current.SavePropertiesAsync();
            await Navigation.PopAsync();
            } else {
               await DisplayAlert("Information Required", "You need to enter a valid email to register as a user", "OK", "Cancel and Exit");
            }
        }

        
        protected  override bool OnBackButtonPressed() {
            Navigation.PopToRootAsync(false);
            return true;
        }

    }
}
